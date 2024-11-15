<script type="text/javascript"> 

    function killBackSpace(e){
    e = e? e : window.event;
    var k = e.keyCode? e.keyCode : e.which? e.which : null;
    if (k == 116 || k == 122|| k == 112 || k == 113||k == 114 || k == 115||k == 117 || k == 118||k == 119 || k == 120||k == 123)
    {
    if (e.preventDefault)
    e.preventDefault();
    e.returnValue=false;   
    e.keyCode=0;
    return false;
    };
    var t = e.target? e.target : e.srcElement? e.srcElement : null;
    if(t && t.tagName && (t.type && /(password)|(text)|(file)/.test(t.type.toLowerCase())) || t.tagName.toLowerCase() == 'textarea')
    return true;
    if (k == 8){
    if (e.preventDefault)
    e.preventDefault();
    return false;
    };
    return true;
    };
   
    
    if(typeof document.addEventListener!='undefined')
    document.addEventListener('keydown', killBackSpace, false);
    else if(typeof document.attachEvent!='undefined')
    document.attachEvent('onkeydown', killBackSpace);
    else{
    if(document.onkeydown!=null){
    var oldOnkeydown=document.onkeydown;
    document.onkeydown=function(e){
    oldOnkeydown(e);
    killBackSpace(e);
    };}
    else
    document.onkeydown=killBackSpace;
    }

    
//<![CDATA[
<!-- Begin
document.oncontextmenu = function(){return false}
// End -->
//]]>



</script> 