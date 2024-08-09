<script setup lang="ts">
import {useSubscription} from '@vue/apollo-composable';
import gql from 'graphql-tag';
import {watch} from "vue";

const PEOPLE_COUNT_UPDATED = gql`
subscription subPeopleCount{
  peopleCount
}
`;

const {result, loading, error} = useSubscription(PEOPLE_COUNT_UPDATED);

watch(result, data => {
      console.log("New message received:", data)
    }
)

</script>

<template>
  <h3>订阅模式</h3>
  <div v-if="loading">Loading...</div>
  <div v-if="error">Error: {{ error.message }}</div>
  <div v-if="result">
    People Count Update : {{ result.peopleCount }}
  </div>
</template>
