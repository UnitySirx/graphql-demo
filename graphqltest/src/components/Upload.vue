<template>
  <div>
    <input type="file" multiple @change="onFileChange"/>
    <button @click="uploadFile">Upload</button>
  </div>
</template>

<script setup lang="ts">
import {ref} from 'vue';
import gql from "graphql-tag";
import {useMutation} from "@vue/apollo-composable";
import uploadFileOperation from "@/graphql/upload_file.graphql"

const file = ref<File | null>(null);

const {mutate: uploadFileMutation} = useMutation(gql`${uploadFileOperation}`);

const onFileChange = (event: Event) => {
  const target = event.target as HTMLInputElement;
  if (target.files && target.files.length > 0) {
    file.value = target.files[0];
    console.log('文件改变：', file.value);
  }
};

const uploadFile = async () => {
  if (!file.value) return;

  try {
    await uploadFileMutation({
      input: {
        file: file.value,
      }
    }).then(res => {
      console.log('文件上传：', res)
    })

  } catch (error) {
    console.error('异常 = ', error);
  }
};
</script>
