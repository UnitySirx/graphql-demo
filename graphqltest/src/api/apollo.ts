import { ApolloClient, InMemoryCache } from '@apollo/client/core';
import { createHttpLink } from '@apollo/client/link/http';
import { DefaultApolloClient } from '@vue/apollo-composable';

const httpLink = createHttpLink({
    uri: '/graphql',
});

const apolloClient = new ApolloClient({
    link: httpLink,
    cache: new InMemoryCache(),
});

export { apolloClient, DefaultApolloClient };