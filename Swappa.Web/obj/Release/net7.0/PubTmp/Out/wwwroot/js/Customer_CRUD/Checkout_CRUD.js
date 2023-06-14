﻿Cart_checkout = new Vue({
    el: '#Cart_checkout',
    data: {
        dataMain: "",
        isCheckedItems: [],
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
        formatCurrency(value) {
            return value.toLocaleString('en-US', {
                style: 'currency',
                currency: 'USD'
            });
        },
        toggleItemChecked(index) {
            this.isCheckedItems[index] = !this.isCheckedItems[index];
        },
        deleteOrdersAndOrderDetails(index) {
            const orderDetailIds = this.dataMain[index].orderDetailIds;
            const orderIds = this.dataMain[index].orderIds;
            const request = {
                orderDetailIds: orderDetailIds,
                orderIds: orderIds
            };
            console.log(request)
            axios.post("/Customer/Home/DeleteOrdersAndOrderDetails", request)
                .then(response => {
                    console.log("Orders and OrderDetails have been deleted successfully.");
                    window.location.reload();
                })
                .catch(error => {
                    console.log("An error occurred while deleting Orders and OrderDetails: ", error);
                });
        }
    },
    computed: {
        totalPrices() {
            return this.dataMain.map((item, index) => {
                const basePrice = item.product_Price * item.product_Quantity;
                return this.isCheckedItems[index] ? basePrice + 39 : basePrice;
            });
        }
    },
    //watch: {
    //    isCheckedItems: {
    //        handler() {
    //            // Hành động muốn thực hiện khi isCheckedItems thay đổi
    //            console.log('isCheckedItems thay đổi');
    //        },
    //        deep: true
    //    }
    //}
})