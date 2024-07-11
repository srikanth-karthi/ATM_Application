function addNumber(number) {
    const input = document.getElementById('numberInput');
    if(number=='00'&&input.value.length==0){
        return
    }
    if(number=='00'&&input.value.length==5){
        return 
    }
    if(input.value.length!=6){
        input.value += number;
    }
}

function cancel() {
    removeTokens();
}
function clearInput(){
    const input = document.getElementById('numberInput');
    if (input.value.length > 0) {
        input.value = input.value.slice(0, -1);
    }

}
function enter() {
    const input = document.getElementById('numberInput');
    if (input.value.length != 6) {
        alert("Enter Valid Card Number")
        errorMessage.classList.remove('hidden');
    }
    else{
        alert('Entered number: ' + input.value);
        window.location.href="login.html"
        input.value = '';
    }
    }


function removeTokens(){
    try{
        localStorage.removeItem('CardId')
        localStorage.removeItem('Token')
    }
    catch{
        
    }
    window.location.href="index.html";
    
}
    