> Registro histórico da primeira inspeção. O bloqueio MCP e as pendências abaixo foram superados na rodada registrada em [VALIDACAO_MCP_2026-10-19.md](VALIDACAO_MCP_2026-10-19.md). Para o jogo estendido e a entrega atual, veja [VALIDACAO_FINAL.md](VALIDACAO_FINAL.md).

# Estabilização do tutorial — 19/10/2026

Atualização: o bloqueio abaixo foi resolvido e a validação real pelo Unity MCP foi concluída. Resultado atual: **PRONTO PARA RUBRICA**. Consulte [a validação posterior e suas evidências](VALIDACAO_MCP_2026-10-19.md). O restante deste arquivo preserva o registro histórico da implementação inicial.

Correções aplicadas; validação de gameplay pendente por indisponibilidade do Unity MCP. A fase não pode ser considerada completamente validada. Nenhuma implementação da rubrica adicional foi iniciada.

## Alterações

- `ProjectSettings/EditorBuildSettings.asset`: MainMenu habilitada no índice 0 e SampleScene no índice 1. O perfil Windows usa a lista global (`m_OverrideGlobalSceneList: 0`).
- `Assets/Scenes/MainMenu.unity`: MenuActions no Canvas ativo; botão Iniciar ligado por callback persistente a `MenuActions.IniciaJogo()`. Título deslocado para não sobrepor o botão.
- `Assets/UIManager.cs`: inicialização de GameController em Awake, a cada carregamento da cena, inclusive ao iniciar diretamente na cena do jogo. Painel ocultado antes dos updates.
- `Assets/Scenes/SampleScene.unity`: Canvas ativo com UIManager e referência serializada ao EndGamePanel inicialmente inativo; texto de vitória e botões Menu e Reiniciar. Callbacks para `MenuActions.Menu()` e `MenuActions.IniciaJogo()`. Reutilizados os componentes de UI e a fonte já existentes no menu.
- `Assets/GameController.cs`: contador não fica negativo após a quarta coleta.
- `Assets/PlayerMovement.cs`: collider coletado desabilitado antes da destruição adiada, prevenindo processamento duplicado. Movimento e reprodução de áudio mantidos.

## Verificações e evidências

| Verificação | Classificação | Evidência e limite |
|---|---|---|
| Compilação dos scripts | validada | O Editor aberto recompilou Assembly-CSharp: `Logs/Editor.log`: `Tundra build success (9.71 seconds), 5 items updated, 1126 evaluated`; também registrado: `ExitCode: 0 Duration: 9s816ms`. Validada pelo log do Editor, não por MCP. |
| Scene List | validada | Inspeção do arquivo de build: 0 MainMenu, 1 SampleScene; ambas habilitadas; Windows sem override. |
| Referências serializadas locais | validada | Verificador de arquivos: MainMenu com 33 IDs únicos e SampleScene com 69; todas as referências locais fileID resolvem. UIManager aponta para EndGamePanel; scripts referenciados pelos GUIDs dos respectivos .meta. Inspeção estática, sem confirmação do Inspector via MCP. |
| Menu e callbacks serializados | validada | MainMenu contém alvo MenuActions e método IniciaJogo; SampleScene contém Menu e IniciaJogo com estado RuntimeOnly e chamada sem argumentos. Cliques em execução ainda não testados. |
| Quatro objetos na cena | validada | Exatamente quatro instâncias do prefab Triangle, com tag Coletavel e collider trigger no prefab. Não comprova coleta em execução. |
| Preservação da cena e prefabs | validada | Comparação com HEAD: todos os blocos originais de gameplay de SampleScene idênticos; apenas UI e entradas de raízes acrescentadas. Player.prefab e Triangle.prefab sem diff. |
| Inicialização direta no Editor | não testada | Correção em Awake inspecionada; falta Play Mode diretamente em SampleScene. |
| Jogabilidade, movimento, áudio e colisões | não testada | Não houve sessão de Play Mode controlada nesta fase. |
| Coleta dos quatro objetos e vitória | não testada | Falta teste real de triggers e observação do contador em Play Mode. |
| Exibição do painel de vitória | não testada | Painel configurado e referência inspecionada; visibilidade e layout em execução não observados. |
| Reinício e retorno ao menu | não testada | Callbacks configurados; falta clicar, retornar e completar uma segunda partida. |
| Conexão Unity MCP | falhou | Endpoint configurado em `http://localhost:8080/mcp`; duas tentativas de initialize, incluindo execução fora do sandbox, retornaram “Impossível conectar-se ao servidor remoto”. Nenhuma ferramenta Unity MCP disponível na sessão; busca de plugins não encontrou integração pertinente. |
| Teste suplementar da DLL por reflexão no PowerShell | falhou | Host PowerShell não carrega a dependência netstandard 2.1. Não representa falha de gameplay, nem comprova estado de vitória. |

## Pendência para encerrar a validação

Restabelecer o servidor Unity MCP no endpoint configurado e disponibilizar suas ferramentas nesta sessão. Então verificar Console e Inspector, executar SampleScene diretamente, coletar os quatro itens por colisão, observar o painel, clicar Reiniciar, repetir a coleta, retornar ao Menu e clicar Iniciar novamente. Confirmar áudio, movimento, paredes e contagem reinicializada durante o fluxo.

Sem tempo, inimigos, vidas, novos gráficos, novos sistemas, build ou publicação nesta fase.
