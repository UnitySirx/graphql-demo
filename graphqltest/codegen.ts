import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  overwrite: true,
  schema: "http://localhost:5293/graphql/",
  documents: "src/**/*.graphql",
  ignoreNoDocuments: true,
  generates: {
    './src/gql/graphql.ts': {
      plugins: [
        'typescript', 'typescript-operations', 'typescript-vue-apollo'
      ],
      config: {
        useTypeImports: true, // 使用 TypeScript 的类型导入语法
        withCompositionFunctions: true, // 生成 Vue Composition API 的代码
        vueCompositionApiImportFrom: 'vue',
      },
    },
    "./schema.graphql": {
      plugins: ["schema-ast"], // 生成完整的 schema 文件
      config: {
        includeDirectives: true,
      },
    },
  },
};

export default config;
