<template>
  <div>
    <button @click="createPerson">Create Person</button>
    <button @click="getPeople">Get People</button>
    <button @click="getPerson">Get Person</button>
    <button @click="updatePerson">Update Person</button>
    <button @click="deletePerson">Delete Person</button>

    <div>
      Person:Data = {{personData?.person?.id}},{{personData?.person?.name}}
    </div>

    <div v-if="peopleData?.people">
      <h2>People List:</h2>
      <ul>
        <li v-for="person in peopleData.people" :key="person.id">{{person.id}} - {{ person.name }}</li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import {useQuery,useMutation} from '@vue/apollo-composable';
import gql from "graphql-tag";
import {reactive} from "vue";
import {cloneDeep} from "@apollo/client/utilities";

import query_people from "@/graphql/query_people.graphql";
import create_person from "@/graphql/create_person.graphql";
import query_person from "@/graphql/query_person.graphql";
import update_person from "@/graphql/update_person.graphql";
import delete_person from "@/graphql/delete_person.graphql";

interface Person {
  id: string;
  name: string;
}

interface PeopleData {
  people: Person[];
}

interface CreatePersonInput {
  name: string;
}

interface UpdatePersonInput {
  name: string;
}

const { result: peopleData, refetch: refetchPeople } = useQuery<PeopleData>(gql`${query_people}`);
const { refetch: refetchPerson } = useQuery<Person>(gql`${query_person}`,{ id: "" });
const { mutate: createPersonMutation } = useMutation<{ createPerson: Person }, { input: CreatePersonInput }>(gql`${create_person}`);
const { mutate: updatePersonMutation } = useMutation<{ updatePerson: Person }, { id: string, input: UpdatePersonInput }>(gql`${update_person}`);
const { mutate: deletePersonMutation } = useMutation<{ deletePerson: boolean }, { id: string }>(gql`${delete_person}`);

const createPerson = async () => {
  await createPersonMutation({
    input: { name: "UnitySir" }
  });
  refetchPeople();
};

const personData = reactive({} as Person)
const getPerson = async () => {
  await refetchPerson({id: "24ac67596bf342d0be0a7339bbd43836"})?.then(res => {
    const cloneData = cloneDeep(res.data);
    Object.assign(personData, cloneData)
    personData.person.name = "Unitysir-JACK" //代码中修改名称
  })
};

const updatePerson = async () => {
  await updatePersonMutation({
    id: "24ac67596bf342d0be0a7339bbd43836",
    input: { name: "3-UnitySir" }
  });
  refetchPeople();
};

const deletePerson = async () => {
  await deletePersonMutation({
    id: "24ac67596bf342d0be0a7339bbd43836"
  });
  refetchPeople();
};

const getPeople = async () => {
  await refetchPeople();
};
</script>