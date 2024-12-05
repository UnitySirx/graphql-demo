<script setup lang="ts">
import {ref} from "vue";
import {useLoginLazyQuery} from "@/gql/graphql.ts";

const {load: loadLogin, refetch: loginRefetch} = useLoginLazyQuery()

const account = ref('')
const password = ref('')

const login = async () => {
  if (account.value && password.value) {
    const model = {user:{password: password.value, username: account.value}};
    const result = loadLogin(null, model);

    if (result) {
      await result.then(res => {
            if (res.login !== '-1' && res.login !== null) {
              localStorage.setItem('token', 'Bearer ' + res.login);
            }
          })
          .catch(err => {
            console.log('Login failed:', err);
          });
    } else {
      await loginRefetch(model)
          ?.then(res => {
            if (res.data.login !== '-1' && res.data.login !== null) {
              localStorage.setItem('token', 'Bearer ' + res.data.login);
            }

          })
          .catch(err => {
            console.log('Login failed:', err);
          });
    }
  }
};
</script>

<template>
  <div>
    <label  for="email">
      账号
    </label>
    <input
        id="email"
        type="email"
        placeholder="请输入账号"
        v-model="account"
    />
  </div>
  <div>
    <label for="password">
      密码
    </label>
    <input
        id="password"
        type="password"
        placeholder="请输入密码"
        v-model="password"
    />
  </div>
  <div>
    <button
        @click="login"
    >
      登录
    </button>
  </div>
</template>

<style scoped>

</style>