<template>
  <div>
    <button @click="createPerson">Create Person</button>
    <button @click="getPeople">Get People</button>
    <button @click="getPerson">Get Person</button>
    <button @click="updatePerson">Update Person</button>
    <button @click="deletePerson">Delete Person</button>

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

const GET_PEOPLE = gql`
  query people {
    people {
      id
      name
    }
  }
`;

const CREATE_PERSON = gql`
  mutation createPerson {
    createPerson(input: { name: "UnitySIr" }) {
      id
      name
    }
  }
`;

const GET_PERSON = gql`
  query person($id: ID!) {
    person(id: $id) {
      id
      name
    }
  }
`;

const UPDATE_PERSON = gql`
  mutation updatePerson($id: ID!, $input: UpdatePersonInput!) {
    updatePerson(id: $id, input: $input) {
      id
      name
    }
  }
`;

const DELETE_PERSON = gql`
  mutation deletePerson($id: ID!) {
    deletePerson(id: $id)
  }
`;

// Queries and mutations
const { result: peopleData, refetch: refetchPeople } = useQuery<PeopleData>(GET_PEOPLE);

const { mutate: createPersonMutation } = useMutation<{ createPerson: Person }, { input: CreatePersonInput }>(CREATE_PERSON);
const { mutate: updatePersonMutation } = useMutation<{ updatePerson: Person }, { id: string, input: UpdatePersonInput }>(UPDATE_PERSON);
const { mutate: deletePersonMutation } = useMutation<{ deletePerson: boolean }, { id: string }>(DELETE_PERSON);

const createPerson = async () => {
  await createPersonMutation({
    input: { name: "UnitySIr" }
  });
  refetchPeople();
};

const getPerson = async () => {
  const { result } = useQuery<{ person: Person }, { id: string }>(GET_PERSON, { id: "95a7d9cd2bf44f0caa7068a93b25ee55" });
  console.log(result.value);
};

const updatePerson = async () => {
  await updatePersonMutation({
      id: "95a7d9cd2bf44f0caa7068a93b25ee55",
      input: { name: "3-UnitySir" }
  });
  refetchPeople();
};

const deletePerson = async () => {
  await deletePersonMutation({
      id: "95a7d9cd2bf44f0caa7068a93b25ee55"
  });
  refetchPeople();
};

const getPeople = async () => {
  await refetchPeople();
};



</script>