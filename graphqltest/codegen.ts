
import type { CodegenConfig } from '@graphql-codegen/cli';
import vue from "@vitejs/plugin-vue";

const config: CodegenConfig = {
  overwrite: true,
  schema: "http://localhost:5293/graphql/",
  documents: "src/**/*.graphql",
  ignoreNoDocuments: true, // for better experience with the watcher
  generates: {
    './src/gql/': {
      preset: 'client',
      config: {
        useTypeImports: true,
        withCompositionFunctions: true,
        vueApolloComposableImportFrom: vue,
      },
      plugins: [
        "@graphql-codegen/typescript-vue-apollo",
      ],
    },
    "./schema.graphql": {
      plugins: ["schema-ast"],
      config: {
        includeDirectives: true,
      },
    },
  }
};

export default config;
