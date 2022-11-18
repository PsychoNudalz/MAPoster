function Test(id) {
    document.getElementById(id).innerHTML = 'Test';
}
function ToTwine(s, id) {
    var sSplit = s.split("\n");
    console.log(sSplit.toString());
    var output = "";
    var temp = "";
    for (var x in sSplit) {
        console.log(sSplit[x]);
        // @ts-ignore
        for (var y in sSplit[x]) {
            temp = sSplit[x][y];
            // if (temp!=" "){
            if (temp == "\`") {
                temp = '\'';
            }
            output += "{{{" + temp + "}}}";
            // }else{
            //     output+=" ";
            // }
        }
        output += "\n";
    }
    console.log(output);
    document.getElementById(id).innerHTML = output;
    return output;
}
