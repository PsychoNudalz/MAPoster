import pyperclip


TAGHEADERS = [" <div id=\"AsciiArt\" class=\"AsciiArt\">\n","\n</div>"]

ERRORCOUNT=0;
# https://codebeautify.org/image-to-ascii-art
def AsciiToTwine(s):
    sSplit = s.split("\n")
    
    BLACKLIST=["\\","$","_","*", "`","[","]"]
    output = TAGHEADERS[0]
    for x in sSplit:
        output += "<p>"
        for y in x:
            temp = y
            if (temp in BLACKLIST):

                output += "{{{" + temp + "}}}"
            else:
                output += temp
        output += "</p>\n"
    output += TAGHEADERS[1]

    print(output)
    pyperclip.copy(output)
    return (output)


def ArtCleanUp(art):
    output = ''
    for x in art.split("\n"):
        for y in x:
            temp = y

            if(temp in ["<",">","{","}"]):
                temp = "|"
            if (temp == "`"):
                temp = "\'"
            output += temp
        output += "\n"
    return output


def ReplaceDollar(art):
    output = ''
    for x in art.split("\n"):
        for y in x:
            temp = y
            if (temp == "$"):
                temp = "#"
    
            output += temp
        output += "\n"
    return output

def InsertTextToArt(art, text):
    output = art
    textIndex = 0
    # artIndex = [0,0]
    textSplit = text.split(" ")
    for x in range(0, len(output) - 1):
        try:
            currnet = output[x]
        except:
            return output;
        replaceChar = "$"

        if(len(textSplit[textIndex])>5):
            print("WARNING: LONG WORD:", textSplit[textIndex])
            # if(textSplit[textIndex]=="nights"):
            #     print("this")

        if (output[x] == replaceChar):
            # artIndex[0]= x;
            i = x;
            flag = False;
            while (i < len(art) and output[i] == replaceChar and not flag):

                i += 1;
                if (i - x > len(textSplit[textIndex]) + 1):
                    output = "".join((output[:x], "<b>" + textSplit[textIndex].upper() + "</b>#", output[i-1:]))
                    textIndex += 1
                    flag = True
                    if (textIndex == len(textSplit)):
                        return output;
    print("ERROR: DID NOT FINISH INSERTING TEXT:",textSplit[textIndex])

    return output;


def ConvertTxt(fileName, text):
    art, fileName = ReadFile(fileName)

    art = ArtCleanUp(art)
    print(art)
    print()
    if (text != ""):
        art = InsertTextToArt(art, text)
        print("\nInsert Complete\n")
        print(art)
    
    art = ReplaceDollar(art)
    art = AsciiToTwine(art)



    pyperclip.copy(art)
    file = open("Modified/" + fileName+"_"+text[0:min(len(text),10)]+".txt", "w+")
    file.write(art)
    file.close()


def ReadFile(fileName):
    # fileName += ".txt"
    file = open("OriginalAscii/" + fileName +".txt","r")
    art = file.read()
    file.close()
    return art, fileName


def DoubleSize(s):
    sSplit = s.split("\n")
    output = ""
    temp = ""
    for x in sSplit:
        temp = ""
        for y in x:
            temp += y * 2
        output += (temp + "\n") * 2
    pyperclip.copy(output)
    return output


def ConvertTxt2(s):
    ConvertTxt(s, "")


# print(DoubleSize(ReadFile("WakeUpTheKids")[0]))
# ConvertTxt("HD","I am a Hair Dresser")
# ConvertTxt("HD", "I wake up And get dressed. I am a Hair Dresser")
# ConvertTxt("WakeUpTheKids","I wake the kids up for school")
# ConvertTxt("DriveTheKids", "I drive the kids to school")
# ConvertTxt("DriveToWork", "I then drive to work")
# ConvertTxt("LunchText", "What to eat?")
# ConvertTxt("HB-Lunch", "You eat a Cheese bread sticks with a cup of Dirty French")
# ConvertTxt("Eat","I prepare my breakfast")
# ConvertTxt("InstantCoffee","I make and drink my 2 instant coffees")
# ConvertTxt("HD-Day","I Resume Cutting hair for clients, listening to stories. Many speak about their days, some Speak of last nights game")
# ConvertTxt("HD-Day-2","Last night game was a close one!")
# ConvertTxt("HD-Day-2","Hell yea brother!!!")
# ConvertTxt("HD-Day-2","Ah, I support the other team...")
# ConvertTxt("DriveToBar","I finished work and drive to a bar")
# ConvertTxt("Bar_1","I enter the bar and place my order")
# ConvertTxt("Beer","I drink my 0% beer, it was nice and relaxing")
ConvertTxt("Bar_2","I look over and see another person")
ConvertTxt("Bar_4","Should I order a drink for him ?")
ConvertTxt("Bar_5","Thank you ...")

# ConvertTxt("DriveToWork","I Make my way back home to rest... waiting for the time to tick to the next day... and the cycle repeats")