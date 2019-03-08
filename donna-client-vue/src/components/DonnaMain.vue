<template>
  <div class="hello">
    <h3>{{ welcomeMessage }}</h3>
    <ul>
      <li v-for="item in messageList" v-bind:key="item.value">
        {{ item }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import Axios from 'axios';

@Component
export default class DonnaMain extends Vue {

  @Prop() private welcomeMessage!: string;

  private messageList: string[] = [];

  private mounted() {
    this.getTranslation();
  }

  private getTranslation() {
    Axios({ method: 'GET', url: 'https://httpbin.org/ip' }).then(
      (result) => {
        this.messageList.push(result.data.origin);
      },
      (error) => {
        this.messageList.push(error);
      },
    );
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}

div.hello {
  color: darkgreen;
}
</style>
