<script language="javascript">
var borrar='';
 function Tecla(sType)
 {
  var sKey,sKeyAscii;
  if(window.event.keyCode!=13)
  {sKey = String.fromCharCode(window.event.keyCode);
   sKeyAscii=window.event.keyCode;
  // alert (window.event.keyCode);
  // alert ("sKey = "+sKey);
  // alert ("sKeyAscii = "+ sKeyAscii);
   
   switch (sType)
   {
    case 'Alphanumeric':
     if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") || (sKey == ".") || (sKey == "@") ||(sKey == "&") || (sKey == " ") || (sKey == "Ñ") || (sKey == "ñ") || (sKey >= "0" && sKey <= "9")  )) {window.event.keyCode = 0;}
     break;
    case 'Character':
     if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") || (sKey == " ") || (sKey == "Ñ") || (sKey == "ñ"))) {window.event.keyCode = 0;}
     break;
    case 'OnlyCharacter':
     if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") )) {window.event.keyCode = 0;}
     break;
    case 'CaractExt':
	 if(((sKey =="¡") || (sKey =="!") ||(sKey == "@") || (sKeyAscii == 92)||(sKey == "^") ||(sKey == ",")||(sKeyAscii == 34)||(sKeyAscii == "`")||(sKey == "]")||(sKey == "[")||(sKey == "/")||(sKey == "$")||(sKey == ":")||(sKey == "=")||(sKey == "?")||(sKey == "¿")||(sKey == "}")||(sKey == "{") ||(sKey == "+")||(sKey == ">")||(sKey == "<")||(sKey == "*")||(sKey == "#")||(sKey == ")")||(sKey == "(")||(sKey == "|")||(sKey == "%")||(sKeyAscii == 180)||(sKey == "~"))) {window.event.keyCode = 0;}	
     break;
    case 'Integer':
     if(!(sKey >= "0" && sKey <= "9")) {window.event.keyCode = 0;}
     break;
    case 'Double':
     if(!((sKey >= "0" && sKey <= "9") || (sKey == ".")|| (sKey == "-"))) {window.event.keyCode = 0;}
     break;
    case 'Date':
     if(!((sKey >= "0" && sKey <= "9") || (sKey == "/"))) {window.event.keyCode = 0;}
     break;
    case 'Time':
     if(!((sKey >= "0" && sKey <= "9"))) {window.event.keyCode = 0;}
     break;
    case 'Telefono':
     if(!((sKey >= "0" && sKey <= "9") || (sKey == " ") || (sKey == "-") )) {window.event.keyCode = 0;}
     break;
    case 'Moneda':
     if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") || (sKey == ".") ||  (sKey == "/") || (sKey == "`") || (sKey == " ") || (sKey == "$") || (sKey == "€") || (sKey == "¥")  || (sKey == "£") || (sKey >= "0" && sKey <= "9") )) { window.event.keyCode = 0;}
     break;
    case 'DireccionIP':
     if(!((sKey >= "0" && sKey <= "9"))) { window.event.keyCode = 0;}
     break;
    case 'Password':
     if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") || (sKey == ".") || (sKey == "@") || (sKey == "Ñ") || (sKey == "ñ") || (sKey >= "0" && sKey <= "9")  )) {window.event.keyCode = 0;}
     break;
    case 'NumeroyLetra':
     if(!((sKey <= "Z" && sKey >= "A") || (sKey <= "z" && sKey >= "a") || (sKey == "Ñ") || (sKey == "ñ") || (sKey >= "0" && sKey <= "9")  )) {window.event.keyCode = 0;}
     break; 
   }
  }
  else
  {
   
  
  if (window.event.keyCode==13)
  {

  }
  else
   {
   window.event.keyCode = 0;
   }
   
  }
 }
 
 
 
 

 
 
 
</script>