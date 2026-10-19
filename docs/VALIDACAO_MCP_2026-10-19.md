# PRONTO PARA RUBRICA

Validação da estabilização em 19/10/2026, via Unity MCP real. Nenhuma correção adicional de scripts, cenas ou prefabs foi necessária nesta rodada. Nenhum recurso da rubrica foi iniciado.

## Resultados

| Verificação | Classificação | Evidência |
|---|---|---|
| Conexão real | validada | HTTP 200 no initialize; servidor mcp-for-unity-server 3.4.9; instância introUnity@991f165f09b42aa1; projectRoot C:/dev/unity/introUnity; Unity 6000.5.8f1. |
| Compilação e Console | validada | Editor sem compilação pendente; entrada e saída de Play Mode bem-sucedidas. Consultas read_console ao longo e ao final do fluxo retornaram zero erros e avisos. |
| Scene List | validada | manage_build scenes retornou MainMenu e SampleScene, nesta ordem, ambas habilitadas. Cenas executadas com buildIndex 0 e 1, respectivamente. |
| Callback Iniciar | validada | Inspeção em memória: Canvas (MenuActions).IniciaJogo, RuntimeOnly; evento pointerClick no botão carregou SampleScene. |
| UIManager, painel e callbacks do resultado | validada | Inspeção em execução: UIManager ativo com endGamePanel resolvido; Button → MenuActions.Menu; RestartButton → MenuActions.IniciaJogo. Ambos executados com sucesso. |
| Início pelo menu | validada | SampleScene: quatro objetos, contador 4, gameOver False, painel False, jogador na posição original (-2.94, 1.54). |
| Coleta dos quatro | validada | Colisões reais em Play Mode reduziram objetos/contador de 4 para 3, 2, 1 e 0. gameOver permaneceu False até a quarta coleta; tornou-se True na quarta. |
| Painel de vitória | validada | Painel ativo após atualização física; screenshot 03-vitoria-1.png mostra “Voce venceu!”, Reiniciar e Menu. Segunda partida também terminou com panel True. |
| Vitória → Menu → Novo jogo | validada | Evento no botão Menu carregou MainMenu, índice 0. Evento no Iniciar carregou SampleScene com quatro objetos, contador 4, gameOver False, painel False e posição inicial restaurada. |
| Segunda partida completa | validada | Nova sequência de quatro colisões: 3, 2, 1, 0 objetos; segunda vitória com contador 0 e painel ativo. |
| Reiniciar | validada | Evento no RestartButton da segunda vitória recarregou SampleScene com quatro objetos, contador 4, gameOver False e painel oculto. |
| Play diretamente em SampleScene | validada | Saída de Play Mode, carregamento de SampleScene e nova entrada sem passar pelo menu: quatro objetos, contador 4, gameOver False, painel False. |
| Integridade das cenas no Editor | validada | manage_scene validate, sem reparos automáticos: zero issues, zero missingScripts, zero brokenPrefabs em ambas as cenas. |

## Método e limites

Os botões foram acionados pelo sistema real de eventos uGUI (`ExecuteEvents.pointerClickHandler`), utilizando os callbacks persistentes existentes. Não foram chamados diretamente os métodos de MenuActions para substituir os botões.

Para cada coleta, o MCP posicionou o Rigidbody2D do jogador sobre o coletável e deixou a simulação física executar OnTriggerEnter2D. Não houve chamadas diretas a GameController.Collect, destruição manual de itens, alteração do contador ou ativação manual do painel. Este teste valida colisão, coleta e fluxo; não é um teste manual de deslocamento por teclado ou gamepad. A reprodução do AudioSource foi observada com isPlaying True durante coletas, sem avaliação auditiva humana.

O Editor inicialmente não avançava a simulação em segundo plano (frame 2). Application.runInBackground foi habilitado apenas em runtime para o teste e restaurado para False. As primeiras tentativas de coleta nesse estado não consumiram itens. O arquivo 02a-simulacao-sem-foco.png registra esse estado e não é evidência de vitória.

A primeira consulta C# de diagnóstico usou GetInstanceID, obsoleto nesta versão; o compilador em memória recusou o trecho. A consulta foi corrigida para dispensar esse método e repetida com sucesso, sem editar o código do jogo. O aviso inicial do MCP sobre batches paralelos foi evitado nas chamadas seguintes; não era erro do jogo.

## Evidências

- [Transcrição JSONL das chamadas e respostas MCP](VALIDACAO_MCP_2026-10-19.jsonl): inclui código de diagnóstico, ações, resultados e timestamps UTC.
- [Menu inicial](evidencias-mcp/01-menu.png).
- [Partida inicial](evidencias-mcp/02-jogo-inicial.png).
- [Vitória](evidencias-mcp/03-vitoria-1.png).
- [Retorno ao menu](evidencias-mcp/04-retorno-menu.png).
- [Novo jogo](evidencias-mcp/05-novo-jogo.png).
- [Segunda vitória](evidencias-mcp/06-segunda-vitoria.png).
- [Play direto na cena](evidencias-mcp/07-play-direto.png).

Nenhum problema bloqueante restante foi encontrado no fluxo solicitado. Teste manual de controles, avaliação auditiva e build WebGL não fazem parte da evidência desta rodada.

Estado final: MainMenu aberta, sem alterações não salvas, fora de Play Mode, sem compilação pendente, Application.runInBackground False e Console sem erros/avisos. Alterações preexistentes no repositório, inclusive manifest e lock de pacotes, foram preservadas.
