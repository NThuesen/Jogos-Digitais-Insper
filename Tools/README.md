# Ferramentas auxiliares

Não são dependências do jogo compilado.

- `CreateOriginalAssets.py`: gera os sprites e WAVs originais. Requer Python e Pillow. Exemplo: `python Tools/CreateOriginalAssets.py Temp/GeneratedAssets`. Compare antes de substituir assets importados; preserve os .meta.
- `serve-build.cjs`: servidor HTTP local, com MIME de wasm e Content-Length. Exemplo: `node Tools/serve-build.cjs Builds 8092`.
- `capture-webgl.cjs` e `webgl-flow.json`: capturas automatizadas por teclado no Edge headless. Instale opcionalmente `npm install --prefix Temp/browser playwright-core --no-audit --no-fund`, depois rode `node Tools/capture-webgl.cjs http://127.0.0.1:8092/CrystalEscape-WebGL/ Tools/webgl-flow.json`. O caminho do Edge é o padrão Windows. A macro depende do ritmo de frames; confira as capturas e o Console. `ok:true` indica execução da ação, não aprovação do resultado do jogo.

Os testes de estado, Input System, callbacks e colisões do Editor foram executados pelo Unity MCP. Código e resultados estão na transcrição `docs/RUBRICA_MCP.jsonl`; não há harness de teste dentro da build publicada.
