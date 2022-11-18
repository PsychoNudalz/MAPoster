function Test(id:string){
    document.getElementById(id).innerHTML='Test';

}


function ToTwine(s:string,id:string){
    let sSplit = s.split("\n");
    console.log(sSplit.toString());
    let output = "";
    let temp ="";
    for (let x in sSplit){
        console.log(sSplit[x])
        // @ts-ignore
        for (let y in sSplit[x]){
            temp = sSplit[x][y];
            // if (temp!=" "){
            if(temp=="\`"){
                temp='\''
            }
            output+="{{{"+temp+"}}}"
            // }else{
            //     output+=" ";
            // }
        }
        output+="\n";
    }
    console.log(output);
    document.getElementById(id).innerHTML=output;
    return output;
}