<template>
<div>
  <b-btn v-on:click="saySomething">Say</b-btn>  
  <h3>{{ welcomeMessage }}</h3>
  <div class="chat">        
    <virtual-list :size="40" :remain="8">
      <item v-for="item in messageList" v-bind:key="item.value">{{ item }}</item>
    </virtual-list>
  </div>
</div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

import axios from 'axios';
import virtualList from 'vue-virtual-scroll-list';

@Component
export default class DonnaMain extends Vue {
  @Prop() private welcomeMessage!: string;

  private messageList: string[] = [];

  private saySomething(): void {
    this.messageList.push('say new thing');
  }

  private mounted() {
    this.getTranslation();
  }

  private getTranslation() {
    axios({ method: 'GET', url: 'https://httpbin.org/ip' }).then(
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

div.chat {
  color: black;
  overflow-y: scroll;  
  height: 300px;
}
</style>
