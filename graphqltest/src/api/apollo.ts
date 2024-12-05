import {ApolloClient, ApolloLink, InMemoryCache, split} from '@apollo/client/core';
import { DefaultApolloClient } from '@vue/apollo-composable';
import createUploadLink from "apollo-upload-client/createUploadLink.mjs";
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { createClient } from 'graphql-ws';
import { getMainDefinition } from '@apollo/client/utilities';
import {onError} from "@apollo/client/link/error";
import {setContext} from "@apollo/client/link/context";

const wsLink = new GraphQLWsLink(createClient({
    url: '/ws',
    lazy: true,
    keepAlive: 10_000,
    retryAttempts: 30,
    connectionParams: async () => {
        const token = localStorage.getItem('token');
        return {
            authorization: token ? `${token}` : '',
        };
    },
}));

/**
 * 请求拦截器
 */
const authLink = setContext((_, {headers}) => {
    const token = localStorage.getItem('token'); // 获取存储的 token
    return {
        headers: {
            ...headers,
            authorization: token ? `${token}` : '',
        },
    };
});

/**
 * 响应拦截器
 */
const loggingLink = new ApolloLink((operation, forward) => {
    return forward(operation).map(response => {
        return response;
    });
});

/**
 * 错误拦截器
 */
const errorLink = onError(({graphQLErrors, networkError, operation, forward}) => {
    if (graphQLErrors) {
        for (const err of graphQLErrors) {
            if (err.extensions?.code === 'AUTH_NOT_AUTHENTICATED') {
                console.error('operation = ',operation);
                // 清除本地 token
                localStorage.removeItem('token');
                // 跳转到登录页
                // router.push({name: '/admin/login'});

                // 可选择刷新 token 或重新请求，下面是刷新 token 示例：
                // const newToken = await refreshAuthToken();
                // localStorage.setItem('auth_token', newToken);
            }
        }
    }

    if (networkError) {
        console.error('Network error:', networkError);
    }

    // 继续发送请求（如果需要）
    return forward(operation);
});

const link = createUploadLink({
    uri: '/graphql',
    headers:{
        'GraphQL-Preflight': '1'
    }
});

const fullLink = authLink.concat(loggingLink).concat(errorLink).concat(link);

const splitLink = split(
    ({query}) => {
        const definition = getMainDefinition(query);
        return (
            definition.kind === 'OperationDefinition' &&
            definition.operation === 'subscription'
        );
    },
    wsLink,
    fullLink,
);

const apolloClient = new ApolloClient({
    link: splitLink,
    cache: new InMemoryCache(),
});

export {apolloClient, DefaultApolloClient};