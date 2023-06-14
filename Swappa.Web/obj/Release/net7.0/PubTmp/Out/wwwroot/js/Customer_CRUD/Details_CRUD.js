Details_View = new Vue({
    el: '#Details_View',
    data: {
        getCart: [],
    },
    methods: {

        AddToCartVue() {
            const formData = new FormData();
            formData.append('ProductIds', $("#ProductId").val());
            formData.append('QuantityClient', $("#QuantityClient").val());


            axios.post("/Customer/Home/AddToCart", formData, {
                headers: {
                   
                }
            }).then(response => {
                // Xử lý phản hồi thành công
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })

                Toast.fire({
                    icon: 'success',
                    title: 'Đã thêm vào giỏ hành thành công'
                });

            })
                .catch(error => {
                    // Xử lý lỗi
                    const Toast = Swal.mixin({
                        toast: true,
                        position: 'top-end',
                        showConfirmButton: false,
                        timer: 3000,
                        timerProgressBar: true,
                        didOpen: (toast) => {
                            toast.addEventListener('mouseenter', Swal.stopTimer)
                            toast.addEventListener('mouseleave', Swal.resumeTimer)
                        }
                    })

                    Toast.fire({
                        icon: 'error',
                        title: 'Đã xảy ra lỗi'
                    });
                    console.error(error);
                });
        },
        GetCart() {
            axios.get("/Customer/Home/GetCart")
                .then(rs => {
                    this.getCart = rs.data;
                    console.log(" this.getCart", this.getCart);
                }).catch(error => {
                    console.error(error);
                });

        }
    }
})