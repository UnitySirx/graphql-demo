import { ApolloClient, InMemoryCache,split } from '@apollo/client/core';
import { DefaultApolloClient } from '@vue/apollo-composable';
import createUploadLink from "apollo-upload-client/createUploadLink.mjs";
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { createClient } from 'graphql-ws';
import { getMainDefinition } from '@apollo/client/utilities';

const wsLink = new GraphQLWsLink(createClient({
    url: '/ws',
    lazy: true,
    keepAlive: 10_000,
    retryAttempts: 30,
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