# Crystal Escape

Projeto individual de Introdução ao Unity / Jogos Digitais, Insper. Extensão pequena do tutorial original: recuperar quatro cristais em um laboratório, com tempo limite, três vidas e drones de segurança.

**Aluno:** Nicholas Thuesen.

## Como jogar

- Mova-se com **WASD**, **setas**, **analógico esquerdo** ou **direcional do gamepad**.
- Confirme o botão selecionado com **Enter** ou **A** do controle Xbox. Mouse também funciona nos menus.
- Recupere os **4 cristais** antes de **90 segundos**. Cada cristal vale **100 pontos**.
- Evite o patrulheiro roxo e o perseguidor vermelho. Você começa com **3 vidas**; após dano há **1,25s de proteção**, sinalizados pelo piscar do jogador.
- A partida termina ao coletar tudo, esgotar o tempo ou perder as vidas. O resultado mostra pontos e tempo final. Use **Jogar novamente** ou **Menu principal**.

## Abrir o projeto

1. Use **Unity 6000.5.8f1**, com **WebGL Build Support** instalado pelo Unity Hub.
2. Abra esta pasta no Hub e aguarde a importação dos pacotes.
3. Abra `Assets/Scenes/MainMenu.unity` e dê Play. `SampleScene` também pode ser executada diretamente.
4. A Scene List deve conter **MainMenu no índice 0** e **SampleScene no índice 1**, habilitadas. Preserve os arquivos `.meta`.

O projeto usa URP 2D, uGUI/TextMesh Pro e Input System. O pacote Unity MCP é utilizado para automação do Editor. O jogo não depende de Python, Node ou MCP em execução no navegador.

## Builds e validação

O progresso e os resultados reais ficam em [Gates e evidências](docs/GATES_CRYSTAL_ESCAPE.md) e [Rubrica](docs/RUBRICA_INTRO_UNITY.md). Não confundir recurso implementado com teste realizado. A build de segurança do tutorial e o backup dos fontes ficam em `Builds/WebGL-Safety` e `Builds/IntroUnity-baseline.zip`; a pasta Builds é ignorada pelo Git.

Resultado final: [validação e evidências](docs/VALIDACAO_FINAL.md). WebGL revisado gerado sem erros e exercitado no Edge; os avisos do Unity e os limites dos testes estão registrados. [Checklist de publicação e Blackboard](docs/CHECKLIST_ENTREGA.md).

Build final: `Builds/CrystalEscape-WebGL`. ZIP HTML5 para envio: `Builds/Release/CrystalEscape-WebGL.zip`. Capa e screenshots: `Builds/Release`. Esses artefatos locais não são versionados; a capa também está em [docs/cover.png](docs/cover.png).

Para abrir a build via HTTP, execute `node Tools/serve-build.cjs Builds 8092` e acesse `http://127.0.0.1:8092/CrystalEscape-WebGL/`. Abrir index.html diretamente por file:// não é o fluxo de execução WebGL.

Para reconstruir no Editor, abra Build Profiles, selecione `Assets/Settings/Build Profiles/WebGL.asset`, confirme Web e a Scene List e use Build. Preserve o template CrystalEscape e os arquivos sem compressão dependente de headers. A build foi validada por seu relatório nativo WebGL, pois o MCP pode identificar incorretamente o alvo do job com o alvo ativo do Editor.

Xbox físico permanece **não testado**, pois o aluno informou que não há controle disponível. Input por gamepad sintético foi exercitado no Editor; teclado e fluxo de partidas foram exercitados no navegador. A conferência humana de volume/qualidade do áudio permanece pendente.

## Código e referências

- `GameController.cs`: contador, score, vidas, tempo e condição de término.
- `PlayerMovement.cs`: leitura dos controles, movimento físico, coleta e proteção após dano.
- `UIManager.cs`: inicialização da partida, HUD e resultado.
- `MenuActions.cs`: callbacks de início/reinício e menu.
- Os componentes de inimigos, câmera e áudio complementam as cenas sem inventário, fases, loja, save ou framework de gameplay.

Arte e áudio novos são procedurais. Fontes, tutorial, áudio original e assistência de IA estão identificados em [CREDITS.md](CREDITS.md).

Repositório: https://github.com/NThuesen/Jogos-Digitais-Insper

Entrega acadêmica: **20/10/2026, 23h50**. Publicar o jogo no itch.io e informar os links no Blackboard são etapas separadas do desenvolvimento local; não estão automaticamente concluídas pela existência deste repositório.
