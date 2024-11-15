@ECHO ------------------------------------------------------------------                                         
jar cvf Print.jar com/hiper/lap/print/*.class resources/*.gif resources/*.png resources/*.jpg
jarsigner -keystore firmaPrint -storepass firmaPrint Print.jar firmaPrint
pause
@ECHO ------------------------------------------------------------------ 
@ECHO                                                                    



