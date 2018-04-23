;#DropDB CalculatedPropertiesSolutionEasyTest

#Application CalculatedPropertiesSolution.Win
#Application CalculatedPropertiesSolution.Web
#IfDef CalculatedPropertiesSolution.Win

*Action About...
 
*Action OK

#EndIf

#IfDef CalculatedPropertiesSolution.Web
!Action Navigation(My Details)
#EndIf
