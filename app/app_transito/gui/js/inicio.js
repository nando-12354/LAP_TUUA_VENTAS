var app = new Vue({
    el:'#app',
    data: {
        titulo: "Pantalla Tránsito",
        lista_vuelos: [],
        lista_vuelos_salida: [],
        pantalla:1,
        vuelo_llegada: {},
        vuelo_salida: {},
        aerolineas: [],
        contador : 0
    },
    methods:{
        get_vuelos_llegada: function(){
            console.log("get_vuelos");
            //console.log(lista_vuelos);
            eel.obtenerVuelosLlegada()(resp=>{
                console.log(this.lista_vuelos);
                this.lista_vuelos=JSON.parse(resp);
                console.log(this.lista_vuelos);

            });
        },
        mostrar_lista_vuelos: function(){
            this.contador = 0;
            eel.obtenerVuelosLlegada()(resp=>{
                console.log(this.lista_vuelos);
                this.lista_vuelos=JSON.parse(resp);
                console.log(this.lista_vuelos);
                this.pantalla = 2;
            });

        },
        regresar: function(){
            this.contador=0;
            if(this.pantalla>1){
                this.pantalla--;
            }
        },
        seleccionar_vuelo_llegada: function(vuelo){
            this.contador=0;
            if(vuelo){
                this.vuelo_llegada= vuelo;
                console.log(this.vuelo_llegada);
                this.pantalla=3;
            }
        },
        mostrar_aerolineas: function(){
            this.contador=0;
            if(this.vuelo_llegada){
                eel.obtenerAerolineasSalida()(resp=>{
                    this.aerolineas = JSON.parse(resp);
                    this.pantalla=4;
                });
                
            };
        },
        mostrar_vuelos_salida: function(iata){
            this.contador =0;
            console.log(`IATA ${iata}`);
            eel.obtenerVuelosSalidaPorAerolinea(iata)(resp=>{
                this.lista_vuelos_salida = JSON.parse(resp);
                console.log(this.lista_vuelos_salida);
                this.pantalla=5;

            });
        },
        seleccionar_vuelo_salida: function(vuelo){
            this.contador=0;
            if(vuelo){
                this.vuelo_salida= vuelo;
                console.log(this.vuelo_salida);

                eel.insertarRegistroTransito(this.vuelo_llegada.Num_Vuelo,this.vuelo_llegada.Fch_Prog,this.vuelo_salida.Num_Vuelo,this.vuelo_salida.Fch_Prog,"","")(resp=>{
                    if(resp=="OK"){
                        //ok
                        this.pantalla=6;
                        cont = 0;
                        var x = setInterval(() => {
                            if(cont==3){
                                this.pantalla=1;
                                clearInterval(x);
                            }
                            cont++;
                        }, 1000);

                    } else{
                        //error
                        cont = 0;
                        var x = setInterval(() => {
                            if(cont==3){
                                this.pantalla=1;
                                clearInterval(x);
                            }
                            cont++;
                        }, 1000);


                    }

                })

            }
        },
    },

});


function reloj() {
    var x = setInterval(() => {
        var now = new Date()
        var strFecha = formatoFecha(now);
        //var texto = document.createTextNode(strFecha);
        document.getElementById("reloj").innerHTML = `<b class='plomo'>  ${strFecha} </b>`;
        if (app.pantalla != 1) {
            app.contador++;
        }

        if (app.contador == 30) {
            app.pantalla = 1;
            app.contador = 0;
        }


    }, 1000);
}
reloj();