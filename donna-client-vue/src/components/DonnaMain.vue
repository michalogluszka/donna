<template>
<div class="main">
  <b-btn v-on:click="saySomething">Say</b-btn>  
  <h3>{{ welcomeMessage }}</h3>
  <div class="chat">        
    <ul>
      <li v-for="item in messageList" v-bind:key="item.value">{{ item }}</li>
    </ul>
  </div>
</div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import Axios from 'axios';

@Component
export default class DonnaMain extends Vue {
  @Prop() private welcomeMessage!: string;

  private messageList: string[] = [];

  private saySomething(): void {
    this.messageList.unshift('say new thing');
  }

  private mounted() {
    this.getTranslation();
  }

  private getTranslation() {
    Axios({ method: 'GET', url: 'https://httpbin.org/ip' }).then(
      (result) => {
        this.messageList.unshift(result.data.origin);
      },
      (error) => {
        this.messageList.unshift(error);
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

div.main {
  height: 100vh;
}

div.chat {
  color: black;
  overflow-y: scroll;    
  height: 400px;
  }
</style>
