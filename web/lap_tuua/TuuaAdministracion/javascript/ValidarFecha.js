<script language="javascript">
function ValidarFecha(caja, etiqueta)
{  var cFec;
   cFec=caja.value;
   borrar = '';
   if (cFec.length >0)
   {   
      borrar = cFec; //01/01/2008
      if ((cFec.substr(2,1) == "/") && (cFec.substr(5,1) == "/"))
      {      
         for (i=0; i<10; i++)
	     {	  
            if (((cFec.substr(i,1)<"0") || (cFec.substr(i,1)>"9")) && (i != 2) && (i != 5))
			{ 
               borrar = '';
               break;  
			}  
         }
	     if (borrar)
	     { 
	        a = cFec.substr(6,4);
		    m = cFec.substr(3,2);
		    d = cFec.substr(0,2);
		    if((a < 1900) || (a > 2050) || (m < 1) || (m > 12) || (d < 1) || (d > 31))
		      { borrar = ''; }
		    else
		    {
		       if((a%4 != 0) && (m == 2) && (d > 28))	   
		         borrar = ''; // Año no biciesto y es febrero y el dia es mayor a 28
			   else	
			   {
		          if ((((m == 4) || (m == 6) || (m == 9) || (m==11)) && (d>30)) || ((m==2) && (d>29)))
			        { borrar = '';  }     				  	 
			   }  // else
		     } // fin else
         } // if (error)
      } // if ((cFec.substr(2,1) == "/") && (cFec.substr(5,1) == "/"))			    			
	  else
	     borrar = '';
	     if (borrar == '')
	        {
	  	     //window.document.forms[0].elements[etiqueta].value = "Error: Formato de fecha incorrecto";
	  	    
	  	     alert('Formato de fecha incorrecto');
	  	     //window.document.forms[0].elements[caja].value = '';
	  	     caja.value= '';
	  	    // window.document.forms[0].elements[cFec].focus;
	  		}
		 /*else		
			{
			  window.document.forms[0].elements[etiqueta].value = "";
			}*/
   } // if (cFec)   
   /*else
   {	window.document.forms[0].elements[etiqueta].value = "";
   }*/
}


    //Valida Hora
    function CheckTime(str)
    {
        hora=str.value
        var valor=str.value
                    
        if (hora=='') {return}
        //if (hora.length>8) {alert("Introdujo una cadena mayor a 8 caracteres");return}
        //if (hora.length!=8) {alert("Introducir HH:MM:SS");return}
        //a=hora.charAt(0) //<=2
        //b=hora.charAt(1) //<4
        //c=hora.charAt(2) //:
        //d=hora.charAt(3) //<=5            
        //e=hora.charAt(4) //>=0
        //f=hora.charAt(5) //:
        //g=hora.charAt(6) //<=5
        //h=hora.charAt(7) //>=0                                
        
       var mytool = valor.split(":");
       var horas=mytool[0];
       var minutos=mytool[1];
       var segundos=mytool[2];       
              
       horas=horas.replace("__","00");
       minutos=minutos.replace("__","00");
       segundos=segundos.replace("__","00");
       
       horas=horas.replace("_","0");
       minutos=minutos.replace("_","0");
       segundos=segundos.replace("_","0");    
       
              
       if (horas>23 && minutos>59 && segundos>59){str.value='23:59:59';}
       if (horas<=23 && minutos>59  && segundos>59){str.value=horas+':59:59';}
       if (horas<=23 && minutos<=59  && segundos>59){str.value=horas+':'+minutos+':59';}
       if (horas<=23 && minutos>59  && segundos<=59){str.value=horas+':59:'+segundos;}
       if (horas>23 && minutos<=59  && segundos<=59){str.value='23:'+minutos+':'+segundos;}
       if (horas>23 && minutos>59  && segundos<=59){str.value='23:59:'+segundos;}
       if (horas>23 && minutos<=59  && segundos>59){str.value='23:'+minutos+':59';}
       
       
       if (horas<=23 && minutos=="00" && segundos=="00"){str.value=horas+'00:00';}
       if (horas<=23 && minutos<=59  && segundos=="00"){str.value=horas+':'+minutos+':00';}
       if (horas<=23 && minutos>59  && segundos=="00"){str.value=horas+':59:00';}
       if (horas>23 && minutos>59  && segundos=="00"){str.value='23:59:00';}       
       if (horas=="__" && minutos=="__"  && segundos=="__"){str.value='__:__:__';}
        
        //if (a>2 && b>4 && d>5 && g>5){str.value='2'+'4'+c+'5'+e+''+f+'5'+h; } //hh:Hora
        //if (a>2 && b>4 && d<=5 && g<=5){str.value='2'+'4'+c+''+d+''+e+''+f+''+g+''+h; } //hh:Hora 
        //if (a<=2 && b>4 && d<=5 && g<=5){str.value=a+'4'+c+''+d+''+e+''+f+''+g+''+h; } //hh:Hora 
        //if (a<=2 && b<=4 && d>5 && g<=5){str.value=a+''+b+''+c+''+'5'+''+e+''+f+''+g+''+h; } //hh:minutos 
        //if (a<=2 && b<=4 && d<=5 && g>5){str.value=a+''+b+''+c+''+d+''+e+''+f+''+'5'+''+h; } //hh:Hora 
              
        //alert(a+'-'+b+'-'+c+'-'+d+'-'+e+'-'+f+'-'+g+'-'+h);
                                
        //if (c!=':' && f!=':'){str.value=a+''+b+':'+d+''+e+':'+g+''+h; return}
        //if (c!=':' && f==':'){str.value=a+''+b+':'+d+''+e+''+f+''+g+''+h; return}
        //if (c==':' && f!=':'){str.value=a+''+b+''+c+''+d+''+e+':'+g+''+h; return}
        
     }
     
     
    function CheckTime2(str)
    {
        hora=str.value
        var valor=str.value
                    
        if (hora=='') {return}
        //if (hora.length>8) {alert("Introdujo una cadena mayor a 8 caracteres");return}
        //if (hora.length!=8) {alert("Introducir HH:MM:SS");return}
        //a=hora.charAt(0) //<=2
        //b=hora.charAt(1) //<4
        //c=hora.charAt(2) //:
        //d=hora.charAt(3) //<=5            
        //e=hora.charAt(4) //>=0
        //f=hora.charAt(5) //:
        //g=hora.charAt(6) //<=5
        //h=hora.charAt(7) //>=0  
        
        if(valor != "__:__:__"){                              
        
		   var mytool = valor.split(":");
		   var horas=mytool[0];
		   var minutos=mytool[1];
		   var segundos=mytool[2];       
	              
		   horas=horas.replace("__","00");
		   minutos=minutos.replace("__","00");
		   segundos=segundos.replace("__","00");
	       
		   horas=horas.replace("_","0");
		   minutos=minutos.replace("_","0");
		   segundos=segundos.replace("_","0");    
	       
	              
		   if (horas>23 && minutos>59 && segundos>59){str.value='23:59:59';}
		   if (horas<=23 && minutos>59  && segundos>59){str.value=horas+':59:59';}
		   if (horas<=23 && minutos<=59  && segundos>59){str.value=horas+':'+minutos+':59';}
		   if (horas<=23 && minutos>59  && segundos<=59){str.value=horas+':59:'+segundos;}
		   if (horas>23 && minutos<=59  && segundos<=59){str.value='23:'+minutos+':'+segundos;}
		   if (horas>23 && minutos>59  && segundos<=59){str.value='23:59:'+segundos;}
		   if (horas>23 && minutos<=59  && segundos>59){str.value='23:'+minutos+':59';}
	       
	       
		   if (horas<=23 && minutos=="00" && segundos=="00"){str.value=horas+'00:00';}
		   if (horas<=23 && minutos<=59  && segundos=="00"){str.value=horas+':'+minutos+':00';}
		   if (horas<=23 && minutos>59  && segundos=="00"){str.value=horas+':59:00';}
		   if (horas>23 && minutos>59  && segundos=="00"){str.value='23:59:00';}       
		   if (horas=="__" && minutos=="__"  && segundos=="__"){str.value='__:__:__';}
	        
			//if (a>2 && b>4 && d>5 && g>5){str.value='2'+'4'+c+'5'+e+''+f+'5'+h; } //hh:Hora
			//if (a>2 && b>4 && d<=5 && g<=5){str.value='2'+'4'+c+''+d+''+e+''+f+''+g+''+h; } //hh:Hora 
			//if (a<=2 && b>4 && d<=5 && g<=5){str.value=a+'4'+c+''+d+''+e+''+f+''+g+''+h; } //hh:Hora 
			//if (a<=2 && b<=4 && d>5 && g<=5){str.value=a+''+b+''+c+''+'5'+''+e+''+f+''+g+''+h; } //hh:minutos 
			//if (a<=2 && b<=4 && d<=5 && g>5){str.value=a+''+b+''+c+''+d+''+e+''+f+''+'5'+''+h; } //hh:Hora 
	              
			//alert(a+'-'+b+'-'+c+'-'+d+'-'+e+'-'+f+'-'+g+'-'+h);
	                                
			//if (c!=':' && f!=':'){str.value=a+''+b+':'+d+''+e+':'+g+''+h; return}
			//if (c!=':' && f==':'){str.value=a+''+b+':'+d+''+e+''+f+''+g+''+h; return}
			//if (c==':' && f!=':'){str.value=a+''+b+''+c+''+d+''+e+':'+g+''+h; return}
        }
     }
     
     //Valida que el Rango de Fechas sea valido
     function isValidoRangoFecha(fecIni, horIni, fecFin, horFin) {
        
        //alert('fecIni ' + fecIni + '   fecFin ' + fecFin);
        var bOk = true;
        var dateIni = new Date();
        dateIni.setYear(parseInt(fecIni.substr(6, 4),10));
        dateIni.setMonth(parseInt(fecIni.substr(3, 2),10) - 1);
        dateIni.setDate(parseInt(fecIni.substr(0, 2),10));
        if (horIni == "__:__:__" || horIni == "" || horIni == "__:__") {
            dateIni.setHours(0);
            dateIni.setMinutes(0);
            dateIni.setSeconds(0);
        }
        else {
            dateIni.setHours(parseInt(horIni.substr(0, 2),10));
            dateIni.setMinutes(parseInt(horIni.substr(3, 2),10));
            if (horIni.length > 6)
                dateIni.setSeconds(parseInt(horIni.substr(6, 2),10));
            else    
                dateIni.setSeconds(0);
        }

        var dateFin = new Date();
        dateFin.setYear(parseInt(fecFin.substr(6, 4),10));
        dateFin.setMonth(parseInt(fecFin.substr(3, 2),10) - 1);
        dateFin.setDate(parseInt(fecFin.substr(0, 2),10));

        if (horFin == "__:__:__" || horFin == "") {
            dateFin.setHours(23);
            dateFin.setMinutes(59);
            dateFin.setSeconds(59);
        }
        else {
            dateFin.setHours(parseInt(horFin.substr(0, 2),10));
            dateFin.setMinutes(parseInt(horFin.substr(3, 2),10));
            if (horFin.length > 6)
                dateFin.setSeconds(parseInt(horIni.substr(6, 2),10));
            else    
                dateIni.setSeconds(0);
        }
        //alert('dateIni ' + dateIni + '  dateFin ' + dateFin);
        if (!(dateIni <= dateFin)) {
            bOk = false;
        }
        return bOk;
    } 
        
    //Validar Hora en Formato hh:mm
    function CheckHora(str)
    {
       var hora=str.value
       var valor=str.value
                    
       if (hora=="" || hora == "__:__") {return}
        
       var mytool = valor.split(":");
       var horas=mytool[0];
       var minutos=mytool[1];
              
       horas=horas.replace("__","00");
       minutos=minutos.replace("__","00");
       
       horas=horas.replace("_","0");
       minutos=minutos.replace("_","0");
              
       if (horas>23 && minutos>59){str.value='23:59';}
       if (horas<=23 && minutos>59){str.value=horas+':59';}
       if (horas<=23 && minutos<=59){str.value=horas+':'+minutos;}
       if (horas<=23 && minutos>59){str.value=horas+':59';}
       if (horas>23 && minutos<=59){str.value='23:'+minutos;}
       if (horas>23 && minutos>59){str.value='23:59';}
       if (horas>23 && minutos<=59){str.value='23:'+minutos;}
       
       if (horas<=23 && minutos=="00"){str.value=horas+':00';}
       //if (horas<=23 && minutos<=59){str.value=horas+':'+minutos;}
       //if (horas<=23 && minutos>59){str.value=horas+':59:00';}
       //if (horas>23 && minutos>59){str.value='23:59:00';}       
       if (horas=="__" && minutos=="__"){str.value='__:__';}
     }
         
         
</script>