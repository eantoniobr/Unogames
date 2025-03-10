# PacketViewer


Utilitário para visualização de packets do jogo

## Observações

Peço por favor para desconsiderarem o péssimo código C#, não é o meu padrão atual. 
Fui mantendo o código bagunçado mas posso garantir que é um excelente utilitário e muito funcional, não vivo sem ele hoje!

Tem excelentes recursos como filtros avançados, filtragem por servidor, gerador de código C# para replicar o packet, já mostra o packet decriptografado, e outros!.

## Installation

Para rodar é necessário a instalação do driver WinPcap (https://www.winpcap.org/install/).

Caso surgir algum erro de referência, as dlls estão todas na pasta "DLLs", basta referenciá-las novamente.

## Download
Caso desejarem baixar uma versão já compilada, deixei ai na pasta principal Build 1.0.0.zip


## Configuração

Como era uma ferramenta só para mim, eu não me preocupei muito com arquivos de configuração, desta forma caso desejarem modificar as portas monitoradas, basta alterar no arquivo do projeto PangyaCapturer.cs

Atualmente as portas configuradas são:

```
  public enum ServiceType : int
  {
      LoginServer = 10303,
      GameServer = 20203,
      RankServer = 4774,
      AntiCheat = 55999
  }
```

## USO

Basta selecionar seu dispositivo de rede (WIFI / WAN) e pressionar Start.

Nenhuma configuração adicional é necessário, os packets são capturados de forma automática quando o usuário faz "login".
Se o jogo já estiver em andamento, é necessário deslogar e efetuar login novamente.

Observação adicional: O visualizador também captura em servidores executando localhost, porém para que seja possível a captura é necessário instalar um "Adaptador Loopback", para que a captura seja realizada.

## Teclas de atalho:

- CTRL + O = Limpar lista de packets
- CTRL + C = Abre janela geradora de código C#