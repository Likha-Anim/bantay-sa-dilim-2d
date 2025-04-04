
-> loob_bahay

// First Scene Dialogues for Bantay sa Dilim

VAR doorClosedCount = 0
VAR hasAcceptedRequest = false

#ChangeSection:Gumising
#PlaySoundLoop:door-knocking
#ChangeUI:Dialogue
#ChangeSpeaker:Narrator
(Malakas na katok mula sa labas ng kubo.)
#ChangeUI:
#AddDelay:1
-> loob_bahay

=== loob_bahay ===
#PlaySoundLoop:door-knocking
#ChangeSection:Kumakatok
#AddDelay:1
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Nggh... Ang ingay... Ang sakit ng ulo ko...
-> unang_desisyon

=== unang_desisyon ===
+ {not hasAcceptedRequest} Buksan ang pinto
    -> buksan_pinto
* {hasAcceptedRequest} Samahan na si Maria pumunta sa Albularyo
    -> samahan_maria
+ Pumunta sa kaliwa
    -> kaliwa
+ Pumunta sa kanan
    -> kanan
    
=== buksan_pinto ===
#ChangeSection:Pintuan
#AddDelay:1
#PlaySound:open-door
#AddDelay:1
#ChangeSection:BuksanPinto
#AddDelay:1
#NextDialogue:

{   doorClosedCount == 0: 
    #ChangeSection:BuksanPintoMaria1
    #ChangeUI:Dialogue
    #ChangeSpeaker:Narrator
    (Pagkabukas ng pinto, makikita si Maria na mukhang balisa.)

    #ChangeSpeaker:Maria
    Tao po! Kailangan ko ng tulong!
}
{   doorClosedCount == 1: 
    #ChangeSection:BuksanPintoMaria2
    #ChangeUI:Dialogue
    #ChangeSpeaker:Narrator
    (Pagkabukas ng pinto, makikita si Maria na mukhang balisa.)

    #ChangeSpeaker:Maria
    Tao po! Kailangan ko ng tulong!
}
{   doorClosedCount == 2:
    #ChangeSection:BuksanPintoMaria3
    #ChangeUI:Dialogue
    #ChangeSpeaker:Narrator
    (Makikita ang lalong pagsama ng anyo ni Maria.)
}
{   doorClosedCount == 3: 
    #ChangeSection:BuksanPintoMaria4
    #ChangeUI:Dialogue
    #ChangeSpeaker:Narrator
    (Nagmumukhang halimaw na si Maria.)
}

-> ikalawang_desisyon

=== ikalawang_desisyon ===
*   Kalma lang, iha. Sabihin mo sa’kin ang problema mo.
    -> tulungan_maria
+   Isara ang pinto.
    -> isara_pinto
    
=== isara_pinto ===
    #PlaySound:close-door
    #ChangeSection:Pintuan
    #NextDialogue:
    #ChangeUI:Dialogue
~doorClosedCount += 1
{ doorClosedCount == 1:
    #ChangeSpeaker:Narrator
    (Isinara ni Ibarra ang pinto sa mukha ni Maria.)
    
    #ChangeSpeaker:Maria
    Bakit mo sinara!? Kailangan ko ng tulong!
}
{   doorClosedCount == 2:
    #ChangeSpeaker:Narrator
    (Isinara ulit ni Ibarra ang pinto sa mukha ni Maria.)

    #ChangeSpeaker:Maria
    Seryoso ka ba!? Nagmamakaawa na ako, tulungan mo ako! 
}
{   doorClosedCount == 3: 
    #ChangeSpeaker:Maria
    Napakasama mong tao! Pagsisisihan mo ito! 
}
{   doorClosedCount == 4: 
    -> labanan_maria
}
{ doorClosedCount < 4:
    #PlaySoundLoop:door-knocking
    #ChangeUI:
    #NextDialogue:
    -> ikatlong_desisyon
}

=== ikatlong_desisyon ===
+ {not hasAcceptedRequest} Buksan ang pinto
    -> buksan_pinto
    
=== labanan_maria ===
#ChangeSpeaker:Maria (Secret Boss)
Ito ang napapala ng mga taong hindi marunong tumulong sa kapwa!

#ChangeSpeaker:Narrator
(Nagkaroon ng masamang epekto ang desisyon ni Ibarra...)
     
#ChangeUI:
#PlaySound:force-open
#AddDelay:6
#ChangeSection:NasiraPinto
#AddDelay:2
#ChangeUI:ToBeContinued
-> DONE
    
=== tulungan_maria ===
#ChangeSection:BuksanPintoMaria1
#ChangeUI:Dialogue

#ChangeSpeaker:Maria
Manong... Parang may kakaiba sa katawan ko... Nag-iiba ang anyo ko!

#ChangeSpeaker:Ibarra
Ha? Kailan pa ‘yan nagsimula?

#ChangeSpeaker:Maria
Pagkauwi ko kahapon galing sa kabilang baryo.

#ChangeSpeaker:Ibarra
May nangyari bang kakaiba doon?

#ChangeSpeaker:Maria
Ah! May nabangga akong matandang babae. Parang may binulong siya bago siya umalis...

#ChangeSpeaker:Ibarra
Hmmm... Maaaring nakulam ka. Huwag kang mag-alala, may kakilala akong albularyo. Maghahanda muna ako bago tayo umalis.

#ChangeUI:
#PlaySound:close-door
#ChangeSection:Pintuan
#AddDelay:1
#ChangeSection:Kumakatok
#NextDialogue:
~hasAcceptedRequest = true
-> unang_desisyon

=== kaliwa ===
#PlaySound:wood-footsteps
#ChangeSection:Kaliwa
#NextDialogue:

{ not hasAcceptedRequest:
#PlaySoundLoop:door-knocking
#AddDelay:1
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Ang dilim na pala...
}
{ hasAcceptedRequest:
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Dumarami na naman ang kaso ng mga maligno...
}
-> ikaapat_desisyon

=== kanan ===
#PlaySound:wood-footsteps
#AddDelay:1
#ChangeSection:Kanan
#NextDialogue:

{ not hasAcceptedRequest:
#PlaySound:empty-stomach
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Gutom na ako... (Tumutunog ang tiyan.)
}
{ hasAcceptedRequest:
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Hmm... Magagamit ko ba ito?
}
-> ikaapat_desisyon

=== ikaapat_desisyon ===
#ReadyItems:Kanan
#NextDialogue:
+ {not hasAcceptedRequest} Bumalik
    #RemoveItems:Kanan
    #PlaySound:wood-footsteps
    #AddDelay:1
    ->  loob_bahay
+ {hasAcceptedRequest} Bumalik
    #RemoveItems:Kanan
    #PlaySound:wood-footsteps
    #AddDelay:1
    #ChangeSection:Kumakatok
    #NextDialogue:
    -> unang_desisyon

=== samahan_maria ===
#ChangeScene:Scene2
-> DONE

-> END


