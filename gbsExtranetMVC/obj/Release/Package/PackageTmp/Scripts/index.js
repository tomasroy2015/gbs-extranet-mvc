$('.toggle').on('click', function() {
  $('.container').stop().addClass('active');
});

$('.close').on('click', function() {
    $('.container').stop().removeClass('active');
    $('#message').html("");
});

$('input[type=text]').on('keyup', function () {
    this.setAttribute('value', this.value);
});

$('input[type=password]').on('keyup', function () {
    this.setAttribute('value', this.value);
});