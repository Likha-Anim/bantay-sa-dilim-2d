// Second Scene Dialogues for Bantay sa Dilim

#ChangeSection:HandaUmalis
#ChangeUI:Dialogue

#ChangeSpeaker:Ibarra
Tara na, iha. Handa na ako.

#ChangeUI:
#ChangeSection:Kalsada
#AddDelay:1

#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Nawa'y gabayan tayo ng Panginoon.
-> unang_desisyon

=== unang_desisyon ===
+ Diretsuhin ang daan
    -> diretso
+ Pumunta sa kaliwa
    -> kaliwa
+ Pumunta sa kanan
    -> kanan
    
=== diretso ===
#ChangeSection:KalsadaUnahan
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Sigurado ako, mapapagaling ka nung kakilala kong albularyo...
-> DONE

=== kaliwa ===
#ChangeSection:Kaliwa
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Gising pa pala si Osang.

-> ikalawang_desisyon

=== kanan ===
#ChangeSection:Kanan
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Baka may kailanganin kami dito.

+ Bumalik
    -> unang_desisyon

=== ikalawang_desisyon ===
+ Bumalik
    -> unang_desisyon

-> END


