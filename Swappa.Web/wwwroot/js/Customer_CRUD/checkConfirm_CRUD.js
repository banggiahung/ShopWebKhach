checkConfirm = new Vue({
    el: '#checkConfirm',
    data: {
        dataMain: [],

    },
    mounted() {
        axios.get("/Customer/Home/GetCart")
            .then((response) => {
                this.dataMain = response.data;
                console.log("this.dataMain", this.dataMain)
                return Promise.resolve();
            })
            .catch(error => {
                console.log(error);
            });
    },
    methods: {
        calculateTotalPrice() {
            let totalPrice = 0;
            this.dataMain.forEach(item => {
                const itemPrice = item.product_Price * item.product_Quantity;
                totalPrice += itemPrice;
            });
            return totalPrice;
        },
        formatCurrency(value) {
            return value.toLocaleString('en-US', {
                style: 'currency',
                currency: 'USD'
            });
        },
    }
})