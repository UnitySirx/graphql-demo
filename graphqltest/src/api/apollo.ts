import { ApolloClient, InMemoryCache,split } from '@apollo/client/core';
import { DefaultApolloClient } from '@vue/apollo-composable';
import createUploadLink from "apollo-upload-client/createUploadLink.mjs";
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { createClient } from 'graphql-ws';
import { getMainDefinition } from '@apollo/client/utilities';

const wsLink = new GraphQLWsLink(createClient({
    url: '/ws', // 替换为你的 GraphQL WebSocket 端点
}));

const link = createUploadLink({
    uri: '/graphql',
    headers:{
        'GraphQL-Preflight': '1'
    }
});

const splitLink = split(
    ({ query }) => {
        const definition = getMainDefinition(query);
        return (
            definition.kind === 'OperationDefinition' &&
            definition.operation === 'subscription'
        );
    },
    wsLink,
    link,
);

const apolloClient = new ApolloClient({
    link: splitLink,
    cache: new InMemoryCache(),
});

export { apolloClient, DefaultApolloClient };