$('#input-enter').keypress(function (event) {

    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {

            const codigo = $("#input-enter").val();
            const url = `https://proxyapp.correios.com.br/v1/sro-rastro/${codigo}`;

            fetch(url).then(response => {
                return response.json();
            }).then(data => {

                const eventosRastreio = data.objetos[0].eventos;

                for (let i = 0; i < eventosRastreio.length; i++) {
                    
                    var sim = new Date(eventosRastreio[i].dtHrCriado);;
                    var formatada = (((sim.getDate() < 10 ? "0" + sim.getDate() : sim.getDate()) + "/" + (sim.getMonth() < 10 ? "0" + (sim.getMonth() + 1) : ( 1 + sim.getMonth()))) + "/" + sim.getFullYear()) + " às " + (((sim.getHours() < 10 ? "0" + sim.getHours() : sim.getHours()) + ":" + (sim.getMinutes() < 10 ? "0" + sim.getMinutes() : sim.getMinutes())) + ":" + (sim.getSeconds() < 10 ? "0" + sim.getSeconds() : sim.getSeconds() ));

                    var script_html = `
                    <div class="container-rastreio">
                        <h1>
                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 24 24" width="24px" height="24px"><g id="surface196033844"><path style="fill: #F25C05;" d="M 12 2 C 6.488281 2 2 6.488281 2 12 C 2 17.511719 6.488281 22 12 22 C 17.511719 22 22 17.511719 22 12 C 22 6.488281 17.511719 2 12 2 Z M 12 4 C 16.429688 4 20 7.570312 20 12 C 20 16.429688 16.429688 20 12 20 C 7.570312 20 4 16.429688 4 12 C 4 7.570312 7.570312 4 12 4 Z M 11 7 L 11 9 L 13 9 L 13 7 Z M 11 11 L 11 17 L 13 17 L 13 11 Z M 11 11 "/></g></svg>
                            ${eventosRastreio[i].descricao}
                        </h1>
                        <p>
                            ${eventosRastreio[i].unidade.endereco.cidade}
                            ${eventosRastreio[i].unidade.endereco.uf}                                                        
                        </p>
                        <p>${formatada}</p>
                        <p>${eventosRastreio[i].unidade.tipo}</p>
                    </div>`

                    $(".input-box-rastreio").append(script_html)
                }
            })

    }
});