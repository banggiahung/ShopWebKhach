NewVue = new Vue({
    el: '#NewVue',
    data: {
        dataMain: [],

    },
    mounted() {
        axios.get("/Admin/Dashboard/getAllProduct2")
            .then((response) => {
                this.dataMain = response.data[0];
                console.log("this.dataMain", this.dataMain)
                return Promise.resolve();
            })
            .catch(error => {
                console.log(error);
            });
    }
})