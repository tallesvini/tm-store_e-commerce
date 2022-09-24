$(".nav-1").on('click', e => {

    console.log("sdkjksjd")
    const geral = $(".show")

    if ($(".show-1").css("display") == "block") {
        $(".show-1").css('display', 'none')
    } else {
        $(".show-1").css("display", "block")
    }
})

$(".nav-2").on('click', e => {

    console.log("sdkjksjd")
    const geral = $(".show")

    if ($(".show-2").css("display") == "block") {
        $(".show-2").css('display', 'none')
    } else {
        $(".show-2").css("display", "block")
    }
})

$(".nav-3").on('click', e => {

    console.log("sdkjksjd")
    const geral = $(".show")

    if ($(".show-3").css("display") == "block") {
        $(".show-3").css('display', 'none')
    } else {
        $(".show-3").css("display", "block")
    }
})

$(".categoria-lancamento-icon-1").on("click", e => {
    const container = $(".lancamento")
    const containerMasc = $(".masculino")

    if (container.css("display") == "none") {
        container.css("display", "block")
    } else {
        container.css("display", "none")
    }
})

$(".categoria-lancamento-icon-2").on("click", e => {
    const container = $(".lancamento")
    const containerMasc = $(".masculino")

    if (containerMasc.css("display") == "none") {
        containerMasc.css("display", "block")
    } else {
        containerMasc.css("display", "none")

    }
})

$(".categoria-lancamento-icon-3").on("click", e => {
    const container = $(".lancamento")
    const containerMasc = $(".masculino")
    const containerFemi = $(".feminino")

    if (containerFemi.css("display") == "none") {
        containerFemi.css("display", "block")
    } else {
        containerFemi.css("display", "none")

    }
})

$(".categoria-lancamento-icon-4").on("click", e => {
    const container = $(".lancamento")
    const containerMasc = $(".masculino")
    const containerFemi = $(".feminino")
    const containerInfa = $(".infantil")

    if (containerInfa.css("display") == "none") {
        containerInfa.css("display", "block")
    } else {
        containerInfa.css("display", "none")

    }
})