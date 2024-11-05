/* eslint-disable */
import * as types from './graphql';
import type { TypedDocumentNode as DocumentNode } from '@graphql-typed-document-node/core';

/**
 * Map of all GraphQL operations in the project.
 *
 * This map has several performance disadvantages:
 * 1. It is not tree-shakeable, so it will include all operations in the project.
 * 2. It is not minifiable, so the string of a GraphQL query will be multiple times inside the bundle.
 * 3. It does not support dead code elimination, so it will add unused operations.
 *
 * Therefore it is highly recommended to use the babel or swc plugin for production.
 * Learn more about it here: https://the-guild.dev/graphql/codegen/plugins/presets/preset-client#reducing-bundle-size
 */
const documents = {
    "mutation createPerson($input: PersonInput!) {\n  createPerson(input: $input) {\n    id\n    name\n  }\n}": types.CreatePersonDocument,
    "mutation deletePerson($id: String!) {\n  deletePerson(id: $id)\n}": types.DeletePersonDocument,
    "query queryPeople {\n  people {\n    id\n    name\n  }\n}": types.QueryPeopleDocument,
    "query queryPerson($id: String!) {\n  person(id: $id) {\n    id\n    name\n  }\n}": types.QueryPersonDocument,
    "mutation updatePerson($id: String!, $input: UpdatePersonInput!) {\n  updatePerson(id: $id, input: $input) {\n    id\n    name\n  }\n}": types.UpdatePersonDocument,
    "mutation uploadFile($input: UploadFileInput!) {\n  uploadFile(input: $input)\n}": types.UploadFileDocument,
};

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 *
 *
 * @example
 * ```ts
 * const query = graphql(`query GetUser($id: ID!) { user(id: $id) { name } }`);
 * ```
 *
 * The query argument is unknown!
 * Please regenerate the types.
 */
export function graphql(source: string): unknown;

/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "mutation createPerson($input: PersonInput!) {\n  createPerson(input: $input) {\n    id\n    name\n  }\n}"): (typeof documents)["mutation createPerson($input: PersonInput!) {\n  createPerson(input: $input) {\n    id\n    name\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "mutation deletePerson($id: String!) {\n  deletePerson(id: $id)\n}"): (typeof documents)["mutation deletePerson($id: String!) {\n  deletePerson(id: $id)\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query queryPeople {\n  people {\n    id\n    name\n  }\n}"): (typeof documents)["query queryPeople {\n  people {\n    id\n    name\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "query queryPerson($id: String!) {\n  person(id: $id) {\n    id\n    name\n  }\n}"): (typeof documents)["query queryPerson($id: String!) {\n  person(id: $id) {\n    id\n    name\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "mutation updatePerson($id: String!, $input: UpdatePersonInput!) {\n  updatePerson(id: $id, input: $input) {\n    id\n    name\n  }\n}"): (typeof documents)["mutation updatePerson($id: String!, $input: UpdatePersonInput!) {\n  updatePerson(id: $id, input: $input) {\n    id\n    name\n  }\n}"];
/**
 * The graphql function is used to parse GraphQL queries into a document that can be used by GraphQL clients.
 */
export function graphql(source: "mutation uploadFile($input: UploadFileInput!) {\n  uploadFile(input: $input)\n}"): (typeof documents)["mutation uploadFile($input: UploadFileInput!) {\n  uploadFile(input: $input)\n}"];

export function graphql(source: string) {
  return (documents as any)[source] ?? {};
}

export type DocumentType<TDocumentNode extends DocumentNode<any, any>> = TDocumentNode extends DocumentNode<  infer TType,  any>  ? TType  : never;