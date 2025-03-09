// First Scene Dialogues for Bantay sa Dilim

VAR doorClosedCount = 0
VAR hasAcceptedRequest = false

#type:Dialogue
#scene:Main
#speaker:None
(Malakas na katok mula sa labas ng kubo.)
-> main

=== main ===
#sound:door_knocking
#speaker:Ibarra
Nggh... Ang ingay... Ang sakit ng ulo ko...
-> main_choices

=== main_choices ===
Ano ang dapat gawin?
+ {not hasAcceptedRequest} Buksan ang pinto.
    -> forward
* {hasAcceptedRequest} Samahan na si Maria pumunta sa Albularyo.
    -> next
+ Pumunta sa kaliwa.
    -> left
+ Pumunta sa kanan.
    -> right
    
=== forward ===
#sound:open_door
-> door_choices
    
=== door_choices ===
{   doorClosedCount < 2: 
#speaker:None
(Pagkabukas ng pinto, makikita si Maria na mukhang balisa.)

#speaker:Maria
Tao po! Kailangan ko ng tulong!
}
{   doorClosedCount == 2: 
         #speaker:None
        (Makikita ang lalong pagsama ng anyo ni Maria.)
}
{   doorClosedCount == 3: 
        #speaker:None
        (Nagmumukhang halimaw na si Maria.)
}

*   Kalma lang, iha. Sabihin mo sa’kin ang problema mo.
    -> good_route
+   Isara ang pinto.
    #scene:Main
    ~doorClosedCount += 1
    { doorClosedCount <= 1:
        #speaker:None
        (Isinara ni Ibarra ang pinto sa mukha ni Maria.)
        
        #speaker:Maria
        Bakit mo sinara!? Kailangan ko ng tulong!
    }
    { doorClosedCount > 1:
        #speaker:None
        (Isinara ulit ni Ibarra ang pinto sa mukha ni Maria.)
    }
    {   doorClosedCount == 2:
        #speaker:Maria
        Seryoso ka ba!? Nagmamakaawa na ako, tulungan mo ako! 
    }
    {   doorClosedCount == 3: 
        #speaker:Maria
        Napakasama mong tao! Pagsisisihan mo ito! 
    }
    {   doorClosedCount == 4: 
        -> secret_route
    }
    { doorClosedCount < 4:
        -> main
    }
    

=== secret_route ===
#speaker:Maria
Ito ang napapala ng mga taong hindi marunong tumulong sa kapwa!

#speaker:None
(Nagkaroon ng masamang epekto ang desisyon ni Ibarra...)
     
#type:Combat
#scene:Secret
-> DONE
    
=== good_route ===
#speaker:Ibarra
Kalma lang, iha. Sabihin mo sa’kin ang problema mo.

#speaker:Maria
Manong... Parang may kakaiba sa katawan ko... Nag-iiba ang anyo ko!

#speaker:Ibarra
Ha? Kailan pa ‘yan nagsimula?

#speaker:Maria
Pagkauwi ko kahapon galing sa kabilang baryo.

#speaker:Ibarra
May nangyari bang kakaiba doon?

#speaker:Maria
Ah! May nabangga akong matandang babae. Parang may binulong siya bago siya umalis...

#speaker:Ibarra
Hmmm... Maaaring nakulam ka. Huwag kang mag-alala, may kakilala akong albularyo. Maghahanda muna ako bago tayo umalis.

#scene:Main
~hasAcceptedRequest = true
-> main_choices

=== left ===
#sound:wood_footstep
#scene:Left
-> left_right_choices

=== right ===
#sound:wood_footstep
#scene:Right
-> left_right_choices

=== left_right_choices ===
Ano ang dapat gawin?
+ Bumalik
    -> main

=== next ===
// load scene here
#loadScene:SecondScene
-> DONE

-> END


