# ECOral_API_Relatorios

Este projeto representa uma abordagem inovadora e multifacetada para a interação com os ecossistemas marinhos, combinando tecnologia de ponta com a conservação ambiental. Ao integrar educação, monitoramento e turismo responsável, visa não apenas aumentar a conscientização sobre as condições dos recifes de corais, mas também promover a participação ativa na preservação desses preciosos habitats marinhos. Através de uma aplicação interativa, os usuários podem acessar em tempo real uma variedade de parâmetros aquáticos, fornecendo não apenas dados valiosos para pesquisa, mas também uma oportunidade única de aprendizado sobre a importância dos recifes de corais e as ameaças que enfrentam. Além disso, o projeto oferece uma experiência de mergulho educacional, onde os participantes não apenas desfrutam da beleza submarina, mas também contribuem para a restauração dos recifes por meio de técnicas inovadoras de fragmentação controlada. Este esforço conjunto de educação, monitoramento e conservação destaca o compromisso com a sustentabilidade e a preservação dos ecossistemas marinhos, ao mesmo tempo em que oferece uma experiência enriquecedora e transformadora para os envolvidos.

## 1° Passo - No Banco de Dados SQL Developer

Após clonar o repositório, abrir o o banco SQL Developer e conectar na nossa conta para rodar os código que estão no.TXT do arquivo da entregável.
Vamos rodar o DROP TABLE Relatorio para que caso haja alguma tabela no banco com esse nome, jáirá ser excluído junto com seus dados.
No meu caso o código deu erro, pois ue já tinha feito o DROP etão não foi excluída nenhuma tabela com esse nome, porque não tinha.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/36b31a37-60ab-4614-891b-c766233c2a31)

Depois do DROP, vamos rodar o CREATE TABLE para criarmos a nossa tabela.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/db96764d-9586-4674-9e2c-70e1b654b803)

Depois do nosso CREATE vamos verificar se a tabela realmente foi criada. Que no nosso caso foi criado sim, porém está aparecendo vazia ainda porque nãoa dicionamos nenhuma informação na nossa tabela.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/3cb3d6b5-950e-4ec5-b85c-2831197bbba5)

## 2° Passo - Abrir o arquivo "ECOral - Relatorios" no Visual Studio

Vamos instalar as bibliotecas necessárias no nosso arquivo para que ele rode sem problemas.
São elas:
  - CsvHelper
  - Oracle.ManagedDataAccess.Core
  - Swashbuckle.AspNetCore
    
![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/b7057f5e-dc2c-482b-9405-88fdb27feafb)

## 3° Passo - Conexão com o Banco 

Depois de instalarmos todas as bibliotecas, vamos verificar a conexão com o banco e caso seja necessário, pode trocar as credenciais para conectar com o seu próprio banco de dados na pasta appsettings.json

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/e1434c34-189c-4fe1-adb0-58d70047ae43)

## 4° Passo - Importação de Dados com CSV

Para importarmos os dados adquiridos pelo sensor de monitoramento com IOT, nós vamos conectar com um CSV que está em nossa pasta e apontar o caminhod o arquivo csv para que a nossa API pegue os dados desse CSV e insira no banco de dados.
O meu caminho no caso é esse abaixo.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/05cabc3a-98a7-4655-8796-39de8ca469ac)

Vamos para a pasta Controller e dentro dela encontraremos o arquivo RelatorioController.cs onde vamos direcionar o caminho para o arquivo csv.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/cad32c24-87f8-4682-ab76-223cc90c5b3a)

5° Passo - Rodar a API e abertura de Swagger

Agora nós iremos rodar a nossa aplicação e o Swagger irá abrir para que possamos testar a nosa API.
Com o Swagger aberto iremos nos deparar com essa imagem.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/c3450c08-773c-4bce-badf-d68d472013ab)

Antes de tudo, vamos confirmar que não há nenhum dado no nosso banco, rodanod o Get Relatorio.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/98d2b49f-f45a-4054-8d2c-49fc07803583)

Depois da verificação que não há nenhum código no banco, nó vamos rodar o nosso API Relatoo Import. Assim todos os dados daquela planilha serão isneridos no banco.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/39060baa-f891-4d4a-91e5-01ea4586eaac)

Para confirmarmos que realmente inseriu no banco, vamos ver no Get do Swagger e no SELECT do SQL Developer.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/fed23dcb-bb90-4413-ac24-818d29e55df2)
![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/00fe8ad1-21f2-43a6-a8d4-8e0d7640dd39)

## 5° Passo - Geração de relatórios filtrados
### 1° - Relatório por Ano

Aqui nós conseguimos puxar um relatório filtrando ele por ano. 

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/47d4edf4-873a-452a-bc50-1be929791178)

### 2° - Relatório por Fator de Condição da Água

Aqui conseguimos trazer um relatório pelo fator que está sendo monitorado, como por exemplo, pH, Temperatura, Turbidez, Oxigênio, entre outros que nós monitoramos para saber a consição da água se está poluída ou não.

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/91215c52-e0f1-4be0-99a6-e772ac88539b)

## 6° Passo - Adicionais

Também temos algum adicionais como um DELETE

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/5da53af0-53b6-4092-8f32-bc1270e08414)


Temos um UPDATE 

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/9374354c-d3fa-441d-b78b-1a82b705fdba)

Também temos um POST único 

![image](https://github.com/ReginaPompeo/ECOral_API_Relatorios/assets/111822109/31090f53-6fd5-41a5-9e58-3ede53d56ba6)

## Fim
Está foi a nossa aplicação em C# usando AspNet e Swagger, espero que tenha gostado! ;)

## RM's

JHONN BRANDON CABRERA TACACHIRI - RM97305

LEONARDO PAGANINI - RM96562  

REGINA CÉLIA POMPEO BATISTA ALVES - RM97032
