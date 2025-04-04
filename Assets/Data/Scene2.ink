// Second Scene Dialogues for Bantay sa Dilim
VAR UsapManangOsang = false
VAR NakitaManananggal = false

#ChangeSection:HandaUmalis
#ChangeUI:Dialogue

#ChangeSpeaker:Ibarra
Tara na, iha. Handa na ako.

#ChangeUI:
#ChangeSection:Kalsada
#AddDelay:1

#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Iha, taga-dito ka ba?

#ChangeSpeaker:Maria
Hindi po, dumayo lang ako rito. Galing po ako sa baryo ng Balintawak.

#ChangeSpeaker:Maria
Narinig ko po kasi na may mabait na tanod dito na makakatulong sa akin.

#ChangeSpeaker:Ibarra
Huwag kang mag-alala, iha. Gagawin ko ang lahat ng aking makakaya para matulungan ka.
-> unang_desisyon

=== unang_desisyon ===
#ChangeUI:
#PlaySound:dirt-footsteps
#ChangeSection:Madilim
#AddDelay:1.5
#ChangeSection:Kalsada
#NextDialogue:
+ Diretsuhin ang daan
    -> diretso
+ Pumunta sa kaliwa
    -> kaliwa
+ Pumunta sa kanan
    -> kanan
    
=== diretso ===
#PlaySound:dirt-footsteps
#ChangeSection:Madilim
#AddDelay:1.5
#ChangeSection:UnahanWala
#AddDelay:1
#NextDialogue:

{ not NakitaManananggal:
~NakitaManananggal = true
#PlaySoundLoop:wings-flapping
#ChangeSection:UnahanKalaban
#AddDelay:1

#ChangeUI:Dialogue
#ChangeSpeaker:Narrator
(Biglang nagpakita ang Manananggal)

#ChangeSpeaker:Ibarra
Aaaahh!!

#ChangeUI:
#PlaySound:wings-flapping
#AddDelay:2
#ChangeSection:UnahanWala

#ChangeUI:Dialogue
#ChangeSpeaker:Narrator
(Nawala bigla ang Manananggal)

#ChangeSpeaker:Ibarra
Huh?… Nakita mo rin ba yun, Maria?

#ChangeSpeaker:Maria
Ang alin po?

#ChangeSpeaker:Ibarra
Ahh, wala... wala.

#ChangeSpeaker:Ibarra
(Namalikmata lang ba ako? Siguro dahil ito sa sinabi ni Manang Osang.)

#ChangeSpeaker:Ibarra
(Kailangan kong pigilan ang takot ko...)
}

-> ikatlong_desisyon

=== kaliwa ===
#PlaySound:dirt-footsteps
#ChangeSection:Madilim
#AddDelay:1.5
#ChangeSection:Kaliwa
#AddDelay:2
#NextDialogue:

{ not UsapManangOsang: 
~UsapManangOsang = true
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Magandang gabi, Manang Osang. Ano pong ginagawa niyo sa labas sa ganitong oras?

#ChangeSpeaker:Manang Osang
May inaayos lang akong mga materyales bago matulog. Hehehehe...

#ChangeSpeaker:Manang Osang
Kayo naman, ano ang ginagawa niyo sa alanganing oras na ito?
 
#ChangeSpeaker:Ibarra
Ah, may pupuntahan lang po kami — yung albularyo.

#ChangeSpeaker:Manang Osang
Ah, ganun ba? Kung ako sa inyo, mag-iingat ako.

#ChangeSpeaker:Manang Osang
May nakakita daw ng manananggal sa baryo na ito. Mwehehehehe...
 
#ChangeSpeaker:Ibarra
Ah, sige po, mauna na po kami. Salamat po sa babala.
}
 
-> unang_desisyon

=== kanan ===
#PlaySound:dirt-footsteps
#ChangeSection:Madilim
#AddDelay:1.5
#ChangeSection:Kanan
#AddDelay:1
#ChangeUI:Dialogue
#ChangeSpeaker:Ibarra
Ano kaya pwedeng makuha o magamit dito...
#ReadyItems:Kanan
#NextDialogue:

-> ikalawang_desisyon

=== ikalawang_desisyon ===
+ Bumalik
    #RemoveItems:Kanan
    -> unang_desisyon
    
=== ikatlong_desisyon ===
#ChangeUI:
#ChangeSection:UnahanWala
#NextDialogue:
+ Pumunta sa kanan
    -> unahan_kanan
+ Bumalik
    -> unang_desisyon

=== unahan_kanan ===
#ChangeSection:UnahanKanan
#PlaySoundLoop:wings-flapping
#AddDelay:1

#ChangeUI:Dialogue
#ChangeSpeaker:Manananggal
Raaaargghhhhh!

#ChangeSpeaker:Manananggal
Pinagbigyan ko na kayo kanina. Ngayon, hindi na kayo makakatakas!

#PlaySound:wings-flapping
#ChangeSpeaker:Manananggal
Ito na ang huling araw niyo sa mundo! Raaaaarghhh!

#ChangeUI:ToBeContinued
-> END


