# Teste de conceito DbUp

Esta aplicação tem o objetivo de testar de uma forma simples a execução de scripts sql em um banco de dados, utilizando a ferramenta de migrations [DbUp](https://dbup.github.io/) 
para [dotnet core](https://dotnet.microsoft.com/download).

## Funcionamento

A aplicação fica escutando uma pasta específica, quando insere-se um arquivo novo _.sql_, o mesmo é executado no banco.

O DbUp verificar a nível de nome de arquivo, então a cada alteração nova de banco, deve-ser ter um arquivo com um nome novo.

O padrão que foi utilizando nesta aplicação é: *anoMesDiaHoraMinuto_ação_descrição-simples* (Ex.: 202001071433_insert_proposta, 202001071434_update_apolice, 202001071435_droptable_proposta, ...)

## Nuget

Foram utilizados os seguintes pacotes:

* [dbup-core](https://www.nuget.org/packages/dbup-core/)
* [dbup-sqlserver](https://www.nuget.org/packages/dbup-sqlserver)

Mais informaões em [docs](https://dbup.readthedocs.io/en/latest/)
