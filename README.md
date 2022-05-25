

# Informatik Projekt Rubiks Cube

~ Paul Sütterlin, 25.05.2022

Der Code für den RubiksCube befindet sich in Rubiks\

Erklärung: Das Projekt 'Rubiks' modelliert einen 3x3x3 Zauberwürfel. \
Zum bewegen / rotieren des Würfels wird eine Sequenz von Move Notations (unterstützte notation: https://de.wikibooks.org/wiki/Zauberwürfel/_3x3x3/_Notation) geparsed 

Mehr Infos zu den Moves: https://jperm.net/3x3/moves

Mögliche Moves:

        U
        D  
        R  
        L 
        F   
        B   
        M    
        E  
        S   
        Rw    
        Lw    
        Uw    
        Dw   
        Fw  
        Bw
        X
        Y
        Z

Sowie alle Moves invertiert mit ' hinter dem Move. \
Zudem gibt es einige Synonyme: 

        x = X'
        y = Y'  
        z = Z'  
        r = Rw
        l = Lw   
        u = Uw   
        d = Dw    
        f = Fw
        b = Bw

Außerdem sind Klammern unterstützt:

        (F R2)3 (R L)2 (F D) => wird geparsed zu: F R R F R R F R R R L R L F D

# Use Cases
 - Würfel erstellen: Zum spielen muss zuerst ein Würfel erstellt werden und richtig initialisiert werden.
 - Rotieren: Ein einzelner Move wird angegeben und auf den Würfel angewandt
 - Move sequenz parsen: Eine Move Sequenz wird geparsed (wie oben angegeben) und auf den Würfel angewandt
 - Scramble: Der Würfel wird mithilfe einer zufällig generierten Liste an moves verdreht


### Würfel erstellen Sequenz Diagramm

![alt (Würfel erstellen Sequenz Diagramm)](https://i.ibb.co/8xzzhGD/Bild-2022-05-25-185759646.png)


### Move Sequenz parsen Sequenz Diagramm

![alt (Move Sequenz parsen Sequenz Diagramm)](https://i.ibb.co/yF88Fbb/Bild-2022-05-25-195250910.png)


# Klassendiagramm

![alt Klassendiagramm](https://i.ibb.co/7t8CTC5/Bild-2022-05-25-204110081.png)



# Konsolenanwendung

Der Rubikscube wird als Würfelnetz dargestellt. \
Der Command solve funktioniert noch nicht. \


Beispiel Algorithmen: https://www.speedsolving.com/wiki/index.php/PLL