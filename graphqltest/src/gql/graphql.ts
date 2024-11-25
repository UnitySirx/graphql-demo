import gql from 'graphql-tag';
import * as VueApolloComposable from '@vue/apollo-composable';
import type * as VueCompositionApi from 'vue';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
export type ReactiveFunction<TParam> = () => TParam;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  Upload: { input: any; output: any; }
};

export type Mutation = {
  __typename?: 'Mutation';
  createPerson: Person;
  createPersonRange: Array<Person>;
  deletePerson: Scalars['Boolean']['output'];
  updatePerson: Person;
  uploadFile: Scalars['String']['output'];
};


export type MutationCreatePersonArgs = {
  input: PersonInput;
};


export type MutationCreatePersonRangeArgs = {
  inputs: Array<PersonInput>;
};


export type MutationDeletePersonArgs = {
  id: Scalars['String']['input'];
};


export type MutationUpdatePersonArgs = {
  id: Scalars['String']['input'];
  input: UpdatePersonInput;
};


export type MutationUploadFileArgs = {
  input: UploadFileInput;
};

export type Person = {
  __typename?: 'Person';
  id: Scalars['String']['output'];
  name: Scalars['String']['output'];
};

export type PersonInput = {
  name: Scalars['String']['input'];
};

export type Query = {
  __typename?: 'Query';
  people: Array<Person>;
  person: Person;
};


export type QueryPersonArgs = {
  id: Scalars['String']['input'];
};

export type Subscription = {
  __typename?: 'Subscription';
  peopleCount: Scalars['Int']['output'];
};

export type UpdatePersonInput = {
  name: Scalars['String']['input'];
};

export type UploadFileInput = {
  file: Scalars['Upload']['input'];
};

export type CreatePersonMutationVariables = Exact<{
  input: PersonInput;
}>;


export type CreatePersonMutation = { __typename?: 'Mutation', createPerson: { __typename?: 'Person', id: string, name: string } };

export type DeletePersonMutationVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type DeletePersonMutation = { __typename?: 'Mutation', deletePerson: boolean };

export type QueryPeopleQueryVariables = Exact<{ [key: string]: never; }>;


export type QueryPeopleQuery = { __typename?: 'Query', people: Array<{ __typename?: 'Person', id: string, name: string }> };

export type QueryPersonQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type QueryPersonQuery = { __typename?: 'Query', person: { __typename?: 'Person', id: string, name: string } };

export type UpdatePersonMutationVariables = Exact<{
  id: Scalars['String']['input'];
  input: UpdatePersonInput;
}>;


export type UpdatePersonMutation = { __typename?: 'Mutation', updatePerson: { __typename?: 'Person', id: string, name: string } };

export type UploadFileMutationVariables = Exact<{
  input: UploadFileInput;
}>;


export type UploadFileMutation = { __typename?: 'Mutation', uploadFile: string };


export const CreatePersonDocument = gql`
    mutation createPerson($input: PersonInput!) {
  createPerson(input: $input) {
    id
    name
  }
}
    `;

/**
 * __useCreatePersonMutation__
 *
 * To run a mutation, you first call `useCreatePersonMutation` within a Vue component and pass it any options that fit your needs.
 * When your component renders, `useCreatePersonMutation` returns an object that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - Several other properties: https://v4.apollo.vuejs.org/api/use-mutation.html#return
 *
 * @param options that will be passed into the mutation, supported options are listed on: https://v4.apollo.vuejs.org/guide-composable/mutation.html#options;
 *
 * @example
 * const { mutate, loading, error, onDone } = useCreatePersonMutation({
 *   variables: {
 *     input: // value for 'input'
 *   },
 * });
 */
export function useCreatePersonMutation(options: VueApolloComposable.UseMutationOptions<CreatePersonMutation, CreatePersonMutationVariables> | ReactiveFunction<VueApolloComposable.UseMutationOptions<CreatePersonMutation, CreatePersonMutationVariables>> = {}) {
  return VueApolloComposable.useMutation<CreatePersonMutation, CreatePersonMutationVariables>(CreatePersonDocument, options);
}
export type CreatePersonMutationCompositionFunctionResult = VueApolloComposable.UseMutationReturn<CreatePersonMutation, CreatePersonMutationVariables>;
export const DeletePersonDocument = gql`
    mutation deletePerson($id: String!) {
  deletePerson(id: $id)
}
    `;

/**
 * __useDeletePersonMutation__
 *
 * To run a mutation, you first call `useDeletePersonMutation` within a Vue component and pass it any options that fit your needs.
 * When your component renders, `useDeletePersonMutation` returns an object that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - Several other properties: https://v4.apollo.vuejs.org/api/use-mutation.html#return
 *
 * @param options that will be passed into the mutation, supported options are listed on: https://v4.apollo.vuejs.org/guide-composable/mutation.html#options;
 *
 * @example
 * const { mutate, loading, error, onDone } = useDeletePersonMutation({
 *   variables: {
 *     id: // value for 'id'
 *   },
 * });
 */
export function useDeletePersonMutation(options: VueApolloComposable.UseMutationOptions<DeletePersonMutation, DeletePersonMutationVariables> | ReactiveFunction<VueApolloComposable.UseMutationOptions<DeletePersonMutation, DeletePersonMutationVariables>> = {}) {
  return VueApolloComposable.useMutation<DeletePersonMutation, DeletePersonMutationVariables>(DeletePersonDocument, options);
}
export type DeletePersonMutationCompositionFunctionResult = VueApolloComposable.UseMutationReturn<DeletePersonMutation, DeletePersonMutationVariables>;
export const QueryPeopleDocument = gql`
    query queryPeople {
  people {
    id
    name
  }
}
    `;

/**
 * __useQueryPeopleQuery__
 *
 * To run a query within a Vue component, call `useQueryPeopleQuery` and pass it any options that fit your needs.
 * When your component renders, `useQueryPeopleQuery` returns an object from Apollo Client that contains result, loading and error properties
 * you can use to render your UI.
 *
 * @param options that will be passed into the query, supported options are listed on: https://v4.apollo.vuejs.org/guide-composable/query.html#options;
 *
 * @example
 * const { result, loading, error } = useQueryPeopleQuery();
 */
export function useQueryPeopleQuery(options: VueApolloComposable.UseQueryOptions<QueryPeopleQuery, QueryPeopleQueryVariables> | VueCompositionApi.Ref<VueApolloComposable.UseQueryOptions<QueryPeopleQuery, QueryPeopleQueryVariables>> | ReactiveFunction<VueApolloComposable.UseQueryOptions<QueryPeopleQuery, QueryPeopleQueryVariables>> = {}) {
  return VueApolloComposable.useQuery<QueryPeopleQuery, QueryPeopleQueryVariables>(QueryPeopleDocument, {}, options);
}
export function useQueryPeopleLazyQuery(options: VueApolloComposable.UseQueryOptions<QueryPeopleQuery, QueryPeopleQueryVariables> | VueCompositionApi.Ref<VueApolloComposable.UseQueryOptions<QueryPeopleQuery, QueryPeopleQueryVariables>> | ReactiveFunction<VueApolloComposable.UseQueryOptions<QueryPeopleQuery, QueryPeopleQueryVariables>> = {}) {
  return VueApolloComposable.useLazyQuery<QueryPeopleQuery, QueryPeopleQueryVariables>(QueryPeopleDocument, {}, options);
}
export type QueryPeopleQueryCompositionFunctionResult = VueApolloComposable.UseQueryReturn<QueryPeopleQuery, QueryPeopleQueryVariables>;
export const QueryPersonDocument = gql`
    query queryPerson($id: String!) {
  person(id: $id) {
    id
    name
  }
}
    `;

/**
 * __useQueryPersonQuery__
 *
 * To run a query within a Vue component, call `useQueryPersonQuery` and pass it any options that fit your needs.
 * When your component renders, `useQueryPersonQuery` returns an object from Apollo Client that contains result, loading and error properties
 * you can use to render your UI.
 *
 * @param variables that will be passed into the query
 * @param options that will be passed into the query, supported options are listed on: https://v4.apollo.vuejs.org/guide-composable/query.html#options;
 *
 * @example
 * const { result, loading, error } = useQueryPersonQuery({
 *   id: // value for 'id'
 * });
 */
export function useQueryPersonQuery(variables: QueryPersonQueryVariables | VueCompositionApi.Ref<QueryPersonQueryVariables> | ReactiveFunction<QueryPersonQueryVariables>, options: VueApolloComposable.UseQueryOptions<QueryPersonQuery, QueryPersonQueryVariables> | VueCompositionApi.Ref<VueApolloComposable.UseQueryOptions<QueryPersonQuery, QueryPersonQueryVariables>> | ReactiveFunction<VueApolloComposable.UseQueryOptions<QueryPersonQuery, QueryPersonQueryVariables>> = {}) {
  return VueApolloComposable.useQuery<QueryPersonQuery, QueryPersonQueryVariables>(QueryPersonDocument, variables, options);
}
export function useQueryPersonLazyQuery(variables?: QueryPersonQueryVariables | VueCompositionApi.Ref<QueryPersonQueryVariables> | ReactiveFunction<QueryPersonQueryVariables>, options: VueApolloComposable.UseQueryOptions<QueryPersonQuery, QueryPersonQueryVariables> | VueCompositionApi.Ref<VueApolloComposable.UseQueryOptions<QueryPersonQuery, QueryPersonQueryVariables>> | ReactiveFunction<VueApolloComposable.UseQueryOptions<QueryPersonQuery, QueryPersonQueryVariables>> = {}) {
  return VueApolloComposable.useLazyQuery<QueryPersonQuery, QueryPersonQueryVariables>(QueryPersonDocument, variables, options);
}
export type QueryPersonQueryCompositionFunctionResult = VueApolloComposable.UseQueryReturn<QueryPersonQuery, QueryPersonQueryVariables>;
export const UpdatePersonDocument = gql`
    mutation updatePerson($id: String!, $input: UpdatePersonInput!) {
  updatePerson(id: $id, input: $input) {
    id
    name
  }
}
    `;

/**
 * __useUpdatePersonMutation__
 *
 * To run a mutation, you first call `useUpdatePersonMutation` within a Vue component and pass it any options that fit your needs.
 * When your component renders, `useUpdatePersonMutation` returns an object that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - Several other properties: https://v4.apollo.vuejs.org/api/use-mutation.html#return
 *
 * @param options that will be passed into the mutation, supported options are listed on: https://v4.apollo.vuejs.org/guide-composable/mutation.html#options;
 *
 * @example
 * const { mutate, loading, error, onDone } = useUpdatePersonMutation({
 *   variables: {
 *     id: // value for 'id'
 *     input: // value for 'input'
 *   },
 * });
 */
export function useUpdatePersonMutation(options: VueApolloComposable.UseMutationOptions<UpdatePersonMutation, UpdatePersonMutationVariables> | ReactiveFunction<VueApolloComposable.UseMutationOptions<UpdatePersonMutation, UpdatePersonMutationVariables>> = {}) {
  return VueApolloComposable.useMutation<UpdatePersonMutation, UpdatePersonMutationVariables>(UpdatePersonDocument, options);
}
export type UpdatePersonMutationCompositionFunctionResult = VueApolloComposable.UseMutationReturn<UpdatePersonMutation, UpdatePersonMutationVariables>;
export const UploadFileDocument = gql`
    mutation uploadFile($input: UploadFileInput!) {
  uploadFile(input: $input)
}
    `;

/**
 * __useUploadFileMutation__
 *
 * To run a mutation, you first call `useUploadFileMutation` within a Vue component and pass it any options that fit your needs.
 * When your component renders, `useUploadFileMutation` returns an object that includes:
 * - A mutate function that you can call at any time to execute the mutation
 * - Several other properties: https://v4.apollo.vuejs.org/api/use-mutation.html#return
 *
 * @param options that will be passed into the mutation, supported options are listed on: https://v4.apollo.vuejs.org/guide-composable/mutation.html#options;
 *
 * @example
 * const { mutate, loading, error, onDone } = useUploadFileMutation({
 *   variables: {
 *     input: // value for 'input'
 *   },
 * });
 */
export function useUploadFileMutation(options: VueApolloComposable.UseMutationOptions<UploadFileMutation, UploadFileMutationVariables> | ReactiveFunction<VueApolloComposable.UseMutationOptions<UploadFileMutation, UploadFileMutationVariables>> = {}) {
  return VueApolloComposable.useMutation<UploadFileMutation, UploadFileMutationVariables>(UploadFileDocument, options);
}
export type UploadFileMutationCompositionFunctionResult = VueApolloComposable.UseMutationReturn<UploadFileMutation, UploadFileMutationVariables>;