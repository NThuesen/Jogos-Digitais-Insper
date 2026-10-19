# Validação e entrega — Crystal Escape

Aluno: **Nicholas Thuesen**. Prazo: **20/10/2026 às 23h50**, horário de São Paulo.

Esta extensão preserva o tutorial e seus prefabs. Os gates 0–3 foram executados antes do gate WebGL. Detalhes em [GATES_CRYSTAL_ESCAPE.md](GATES_CRYSTAL_ESCAPE.md), [rubrica](RUBRICA_INTRO_UNITY.md) e transcrição [RUBRICA_MCP.jsonl](RUBRICA_MCP.jsonl).

| Verificação | Classificação | Evidência / limite |
|---|---|---|
| Conexão real ao Unity MCP | validada | Editor 6000.5.8f1, instância introUnity@991f165f09b42aa1; comandos de cena, Play e build executados. |
| Compilação C# | validada | Revisão EnemyMotor recompilada; nova sessão de Play com zero entradas no Console. Build nativa final: zero erros e quatro avisos, discriminados abaixo. |
| Scene List / perfil Web | validada | MainMenu 0, SampleScene 1, habilitadas; WebGL.asset usa lista global e alvo WebGL. |
| Callbacks serializados | validada | Iniciar e Restart → Canvas/MenuActions.IniciaJogo; Menu → Canvas/MenuActions.Menu, RuntimeOnly. |
| Referências / prefabs | validada | UIManager ativo, painel/HUD/título/detalhes/restart atribuídos; cenas sem missing scripts ou broken prefabs. Overrides do jogador e cristais sobrevivem à recarga. |
| Play direto na cena do jogo | validada | Awake reinicializa contagem real, score, três vidas e 90s; não depende do menu. |
| Quatro coletas / vitória | validada | Triggers físicos produziram score400, remaining0, Victory e painel ativo; inimigos ativos. |
| Restart / menu / nova partida no Editor | validada | Callback de restart restaurou score0, vidas3, quatro objetos e painel fechado; Menu retornou a MainMenu índice0. |
| Tempo / derrota | validada | Além do teste acelerado do Editor, espera real de 90s na build WebGL exibiu derrota por tempo e tempo final90,0s; restart restaurou 90s. |
| Dano / derrota / proteção | validada | Três danos espaçados, duplicados rejeitados, vidas3→0; contato do perseguidor também causou derrota. |
| Inimigos / rotas / paredes | validada | Patrulha e perseguição em execução; quatro rotas acessíveis; teste físico de parede bloqueou passagem. |
| Input teclado / gamepad simulado | validada | Input System real, diagonal sem ganho de velocidade; controles sintéticos removidos após teste. |
| Xbox físico no Editor/WebGL | não testada | Aluno informou que não há controle disponível. Suporte implementado e input sintético não equivalem a teste de hardware. |
| Música / SFX em runtime | validada | Fontes em reprodução, clips e eventos distintos, redução de música ao terminar. |
| Qualidade e volume percebidos do áudio | não testada | Requer audição humana após interação no navegador. |
| Build WebGL de segurança | validada | Builds/WebGL-Safety; menu aberto no Edge. Backup CRC válido em Builds/IntroUnity-baseline.zip. |
| Build WebGL revisada / navegador | validada | Build nativa Succeeded/WebGL; duas vitórias, restart, menu, nova partida e timeout de90s observados no Edge. Zero erros HTTP/runtime; um aviso URP descrito abaixo. |
| ZIP HTML5 | validada | index.html na raiz, sete arquivos incluindo créditos e OFL; CRC íntegro e SHA256 em PACKAGE_VERIFICATION.json. |
| GitHub público | validada | Metadados em GITHUB_STATUS.json; não prova envio das mudanças locais. |
| GitHub atualizado / itch.io / jam / Blackboard | não testada | Etapas externas pendentes; roteiro em CHECKLIST_ENTREGA.md. |

## Diagnósticos encontrados e tratados

- A primeira build WebGL foi bem-sucedida e venceu no Edge (400 pontos, quatro cristais, duas vidas). A segunda macro, com janela 4:3, parou a poucos pixels do último cristal. Isso foi uma falha da rota temporizada de teste, não evidência de coletável inacessível; adicionada correção de movimento à macro, a conferir na revisão final.
- Dois erros HTTP 404 vieram exclusivamente de `/favicon.ico`, identificados pelo Console com URL. Adicionado favicon embutido no template. O servidor auxiliar ganhou Content-Length para eliminar seu aviso de download.
- EnemyMotor utilizava FindFirstObjectByType, obsoleto na versão instalada. Substituído por FindAnyObjectByType; recompilação e nova vitória no Editor aprovadas.
- Na primeira build ocorreram quatro asserts de import workers, `State comes from an incompatible keyword space` entre CoreBlit e InternalErrorShader. Transcrição preservada antes de limpar o Console para isolar a sessão nova. Não foram erros C# nem falhas do relatório nativo de build. A nova sessão de gameplay e a build revisada não os repetiram.
- O WebGL em Edge headless/SwiftShader avisou que o shader FSR EASU não é suportado. O código do URP instalado constrói esse passe incondicionalmente em Runtime/PostProcess.cs:68; a câmera do jogo tem pós-processamento desativado. A arena e a UI renderizaram corretamente. Preservado o pipeline; o aviso não foi ocultado nem classificado como erro de gameplay.
- Os quatro avisos da build revisada foram três mensagens IL2CPP sobre métodos grandes de TextMesh Pro e uma sobre alterações ainda não compiladas para importadores/pós-processadores do Editor. Não foram alterados scripts de importação/pós-processamento. Conferido no C++ IL2CPP efetivamente gerado que EnemyMotor_Start usa FindAnyObjectByType, portanto a correção está na build; trecho em evidencias-webgl-revisado/linked-EnemyMotor.txt. Refresh posterior concluído; Editor sem compilação pendente, MainMenu salva e fora de Play.

## Resultado do gate WebGL

Relatório nativo em [BUILD_FINAL.json](BUILD_FINAL.json): Succeeded, WebGL, 59.863.577 bytes, 605,27s, zero erros e quatro avisos. Job MCP build-b4ec5bc6f9. Primeira build final preservada como diagnóstico no histórico; a revisão é a versão do ZIP.

Inspeção real das imagens em evidencias-webgl-revisado:

- [Menu inicial](evidencias-webgl-revisado/00-loaded.png).
- [Primeira vitória](evidencias-webgl-revisado/01-vitoria.png): 400 pontos,4/4,duas vidas,10,3s.
- [Restart](evidencias-webgl-revisado/02-reinicio.png): 0 pontos,0/4,90s,três vidas.
- [Segunda vitória, janela4:3](evidencias-webgl-revisado/06-vitoria-4x3.png): 400 pontos,4/4,duas vidas,8,9s.
- [Menu após vitória](evidencias-webgl-revisado/07-menu.png) e [novo jogo](evidencias-webgl-revisado/08-novo-jogo.png), com reset completo.
- [Derrota após90s reais](evidencias-webgl-revisado/09-derrota-tempo-real.png) e [restart após derrota](evidencias-webgl-revisado/10-reinicio-apos-derrota.png).

Os nomes03-vitoria-apos-reinicio.png e04-menu.png eram expectativas da macro: as imagens mostram a partida ainda em andamento, com3/4. O perseguidor interferiu na rota temporizada; a continuação por teclado após redimensionar concluiu a segunda vitória em06. Não são evidências de vitória/menu nesses passos03/04, nem motivo para alterar a colisão correta do jogo. A prova de menu é07. O browser-console.json terminou com errors=[], sem404; o único aviso foi FSR do URP. As mensagens de carregamento de metadados de áudio foram logs, sem exceção. Audição humana continua pendente.

## Reprodução das evidências

Editor: ferramentas Unity MCP manage_scene, manage_editor, read_console, manage_build e execute_code. Requests, código C# temporário e resultados estão na transcrição JSONL. As coletas automatizadas reposicionaram o Rigidbody sobre os cristais para gerar OnTrigger real; não chamaram Collect diretamente.

Navegador: Edge headless com WebGL2/SwiftShader; ações de teclado reais e screenshots, sem alterar o estado do jogo por JavaScript. Comandos: `node Tools/serve-build.cjs Builds 8092` e `node Tools/capture-webgl.cjs http://127.0.0.1:8092/CrystalEscape-WebGL/ Tools/webgl-flow.json`. O roteiro captura reinício, menu, nova partida, janela 4:3 e espera de tempo real. A macro exige inspeção das imagens: ação executada não significa resultado aprovado.

## Artefatos e operação humana

Build: `Builds/CrystalEscape-WebGL`. Pacote pronto: `Builds/Release/CrystalEscape-WebGL.zip`, 19.061.960 bytes. SHA256: `c410a6e3087fd3f568ae872f8af9d66f39faef0f5e9b1d06951637274264c16e`. Verificação em [PACKAGE_VERIFICATION.json](PACKAGE_VERIFICATION.json). Material: capa e capturas em `Builds/Release`, [texto da página](ITCH_IO_TEXTO.md), [créditos](../CREDITS.md) e [checklist](CHECKLIST_ENTREGA.md).

Nenhum upload, push ou envio acadêmico é declarado concluído. O PDF contém divergência entre os pontos listados e a fórmula; não há promessa de nota.
