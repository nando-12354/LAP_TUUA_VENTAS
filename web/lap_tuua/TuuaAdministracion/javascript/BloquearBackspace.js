<SCRIPT LANGUAGE="JavaScript"> 
function getKey(keyStroke) { 
isNetscape=(document.layers); 
eventChooser = (isNetscape) ? keyStroke.which : event.keyCode; 
if (eventChooser==8) { 
return false; 
} 
} 
document.onkeypress = getKey; 

document.onkeydown = function(){
if(window.event && window.event.keyCode == 8)
{
window.event.keyCode = 505;
}
if(window.event && window.event.keyCode == 505)
{
return false;
}
}
</script>
