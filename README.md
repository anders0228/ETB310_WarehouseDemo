# ETB310_WarehouseDemo
_OBS: Hänger ihop med detta repo:
https://github.com/anders0228/ETB310_wholesalerDemo_

Ett litet exempel på en CRUD WCF-service. (använder sig av repositoryt ETB310_wholesalerDemo)
 

Ett exempel på en web-service för ett lager: 

https://github.com/anders0228/ETB310_WarehouseDemo 

 

Lager-web-servicen behöver en grossist-service också: 

https://github.com/anders0228/ETB310_wholesalerDemo 

 

Gör såhär: 

### 1: Ladda ner och packa upp båda web-servicearna 


### 2: Wholesaler 
    - Öppna Wholesaler i Visual Studio 
    - Kör en Build, så att Entity Framework installeras (ctrl+skift+B)
    - Markera filen WholesalerService.svc 
    - Starta servicen (tryck F5) 
    - Testa att anropa funktionen i  WCF Test Client 

### 3: Warehouse 

    - Öppna Warehouse i Visual Studio 
    - Kör en Build, så att Entity Framework installeras
    - Markera filen WarehouseService.svc 
    - Starta servicen (tryck F5) 
    - Testa att anropa funktionerna i WCF Test Client 

 
