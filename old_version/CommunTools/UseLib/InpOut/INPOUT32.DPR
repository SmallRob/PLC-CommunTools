{Source code for inpout32.dll.
Enables 32-bit Visual Basic programs to do direct port I/O
(Inp and Out) under Windows 95.
To be compiled with Borland's Delphi 2.0.}
library inpout32;
uses SysUtils;
procedure Out32(PortAddress:smallint;Value:smallint);stdcall;export;
var
   ByteValue:Byte;
begin
     ByteValue:=Byte(Value);
     asm
        push dx
        mov dx,PortAddress
        mov al, ByteValue
        out dx,al
        pop dx
     end;
end;

function Inp32(PortAddress:smallint):smallint;stdcall;export;
var
   ByteValue:byte;
begin
   asm
        push dx
        mov dx, PortAddress
        in al,dx
        mov ByteValue,al
        pop dx
    end;
    Inp32:=smallint(ByteValue) and $00FF;
end;
Exports
       Inp32,
       Out32;
begin
end.
