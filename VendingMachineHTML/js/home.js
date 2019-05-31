$(document).ready(function(){
  $('#money-input').val("0.00");
  loadItems();
});

function loadItems() {
  $.ajax({
    type: 'GET',
    url: 'http://localhost:8080/items',
    success: function(data, status){
      $.each(data, function(index, item){
        $('#item-' + item.id + '-id').text(item.id);
        $('#item-' + item.id + '-name').text(item.name);
        $('#item-' + item.id + '-price').text('$' + item.price.toFixed(2));
        $('#item-' + item.id + '-quantity').text(item.quantity);
      });
    },
    error: function(){
      alert('And It Failed!');
    }
  })
};

function addMoney(moneyAmount){
  var currentMoney = $('#money-input').val();
  $('#money-input').val((parseFloat(currentMoney) + moneyAmount).toFixed(2));
}

function pickItem(itemId) {
  $('#item-display').val(itemId);
}

function makePurchase(){
  var moneyAmount = $('#money-input').val();
  var itemId = $('#item-display').val();
  $.ajax({
    type: 'GET',
    url: 'http://localhost:8080/money/' + moneyAmount + '/item/' + itemId,
    success: function(data){
      $('#message-display').val('Thank You!!!');
      var returnChange = ((data.quarters + " Quarters ") + (data.dimes + " Dimes ") + (data.nickels + " Nickels "));
      $('#money-input').val('0');
      $('#change-display').val(returnChange);
    },
    error: function(jqXHR){
      $('#message-display').text(JSON.parse(jqXHR.responseText).message);
    }
  });
};

function makeChange() {
  $('#change-display').val('0');
  var moneyAmount = Math.floor($('#money-input').val() * 100);
  if(moneyAmount > 0){
    var quarters = Math.floor(moneyAmount / 25);
    moneyAmount %= 25;
    var dimes = Math.floor(moneyAmount / 10);
    moneyAmount %= 10;
    var nickels = Math.floor(moneyAmount / 5);
    var pennies = moneyAmount % 5;
    $('#change-display').val((quarters + " Quarters ") + (dimes + " Dimes ") + (nickels + " Nickels ") + (pennies + " Pennies "));
  }
  $('#money-input').val('0.00');
};
