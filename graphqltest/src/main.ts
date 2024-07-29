import {createApp, h, provide} from 'vue'
import './style.css'
import App from './App.vue'
import {apolloClient, DefaultApolloClient} from "./api/apollo.ts";

const app = createApp({
    setup() {
        provide(DefaultApolloClient, apolloClient);
    },
    render: () => h(App),
});

app.mount('#app')
