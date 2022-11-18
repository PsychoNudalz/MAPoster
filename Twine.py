import pyperclip
def AsciiToTwine(s):
    sSplit = s.split("\n")
    output = ""
    for x in sSplit:
        for y in x:
            temp = y
            if(temp=="\\"or temp=="$"or temp=="_"):
                if(temp=="`"):
                    temp = "\'"
                output+="{{{"+temp+"}}}"
            else:
                output+=temp
        output+="\n"
    print(output)
    pyperclip.copy(output)
    return (output)

def ArtCleanUp(art):
    output = ''
    for x in art.split("\n"):
        for y in x:
            temp = y
            if (temp=="$"):
                temp = "#"
            output += temp
        output+="\n"
    return  output

def InsertTextToArt(art,text):
    output = art
    textIndex = 0
    # artIndex = [0,0]
    textSplit = text.split(" ")
    for x in range(0,len(output)-1):
        try:
            currnet = output[x]
        except:
            return output;
        if (output[x]=="$"):
            # artIndex[0]= x;
            i = x;
            flag = False;
            while (i<len(art) and output[i]=="$" and not flag):

                i+=1;
                if (i-x>len(textSplit[textIndex])+1):
                    output = "".join((output[:x],"<b>"+textSplit[textIndex].upper()+"</b>#",output[i:]))
                    textIndex+=1
                    flag = True
                    if(textIndex==len(textSplit)):
                        return output;
    return output;

def ConvertTxt(fileName, text):
    fileName+=".txt"
    file = open("OriginalAscii/"+fileName,"r")
    art = file.read()
    if(text!=""):
        art= InsertTextToArt(art,text)
        print("\nInsert Complete\n")
        print(art)
    art = ArtCleanUp(art)
    newAscii = AsciiToTwine(art)
    file.close()
    file = open("Modified/" + fileName, "w+")
    file.write(newAscii)
    file.close()

def ConvertTxt2(s):
    ConvertTxt(s,"")

# ConvertTxt("HD","I am a Hair Dresser")
ConvertTxt("HD", "I wake up And get dressed. I am a Hair Dresser")
