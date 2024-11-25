<template>
  <div>
    <button @click="createPerson">Create Person</button>
    <button @click="getPeople">Get People</button>
    <div>
      <input type="text" v-model="personInput">
      <button @click="getPerson">Get Person</button>
      <button @click="updatePerson">Update Person</button>
      <button @click="deletePerson">Delete Person</button>
    </div>

    <div>
      Person:Data = {{ personData?.id }},{{ personData?.name }}
    </div>

    <div v-if="peopleData?.people">
      <h2>People List:</h2>
      <ul>
        <li v-for="person in peopleData.people" :key="person.id">{{ person.id }} - {{ person.name }}</li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import {onMounted, reactive, ref} from "vue";
import {cloneDeep} from "@apollo/client/utilities";

import {
  Person,
  useCreatePersonMutation,
  useDeletePersonMutation, useQueryPeopleLazyQuery, useQueryPersonLazyQuery,
  useUpdatePersonMutation
} from "../gql/graphql.ts";

const {load: personLoad, refetch: refetchPerson} = useQueryPersonLazyQuery();
const {mutate: createPersonMutation} = useCreatePersonMutation();
const {mutate: updatePersonMutation} = useUpdatePersonMutation();
const {mutate: deletePersonMutation} = useDeletePersonMutation();

const {load: load, result: peopleData, refetch: refetchPeople} = useQueryPeopleLazyQuery();

const createPerson = async () => {
  await createPersonMutation({
    input: {name: "UnitySir"}
  });
  await refetchPeople()
};

const personInput = ref('')
const personData = reactive({} as Person)
const getPerson = async () => {
  const result = personLoad(null, {id: personInput.value.trim()})
  if (result) {
    await result.then(res => {
      const cloneData = cloneDeep(res);
      Object.assign(personData, cloneData)
      personData.id = cloneData.person.id
      personData.name = "Unitysir-JACK-001-" + personInput.value.trim(); //代码中修改名称
    })
  } else {
    await refetchPerson({id: personInput.value.trim()})
        ?.then(res => {
          const cloneData = cloneDeep(res.data);
          Object.assign(personData, cloneData)
          personData.id = cloneData.person.id
          personData.name = "Unitysir-JACK-002-" + personInput.value.trim(); //代码中修改名称
        })
  }
};

const updatePerson = async () => {
  await updatePersonMutation({
    id: personInput.value.trim(),
    input: {name: "1-UnitySir"}
  });
  refetchPeople();
};

const deletePerson = async () => {
  await deletePersonMutation({
    id: personInput.value.trim(),
  });
  refetchPeople();
};

const getPeople = async () => {
  await refetchPeople();
};

onMounted(async () => {
  await load();

})
</script>