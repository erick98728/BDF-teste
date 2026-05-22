# PAUSE_MENU_SETUP

## Objetivo
Montar um menu de pausa simples para testes do prototipo em PC. O menu pausa com `ESC`, retoma o jogo, recarrega a cena atual e solicita a saida do jogo em build.

## Estrutura recomendada
Use o Canvas de HUD existente ou crie um Canvas novo:

```text
Canvas
|-- HUDRoot
|-- PauseMenu
|-- PausePanel
    |-- PauseTitle
    |-- Btn_Continuar
    |-- Btn_Reiniciar
    |-- Btn_Sair
```

`PauseMenu` deve ficar ativo durante o Play Mode. Nao coloque `PauseMenuController` dentro de `PausePanel` se o painel comecar desativado, porque o controlador precisa continuar ouvindo `ESC`.

## Montar o Canvas
1. Crie um `Canvas` se a cena ainda nao tiver um.
2. Use `Render Mode = Screen Space - Overlay`.
3. Adicione ou mantenha um `EventSystem` para os botoes receberem clique.
4. Se o HUD ja estiver no Canvas, mantenha o menu de pausa como irmao do HUD para a montagem continuar simples.

## Criar `PauseMenu`
1. Crie um GameObject ativo chamado `PauseMenu` dentro do Canvas.
2. Adicione `PauseMenuController`.
3. Mantenha `Pause Key = Escape`.
4. Arraste `PausePanel` para o campo `Pause Panel`.

## Criar `PausePanel`
1. Crie um `Panel` chamado `PausePanel`.
2. Deixe `PausePanel` desativado antes de entrar em Play Mode.
3. Use uma imagem simples de fundo ou o visual padrao de Panel.
4. Dentro dele, crie um texto simples `PauseTitle` com `Jogo Pausado`.
5. Crie tres `Button` simples:
   - `Btn_Continuar`;
   - `Btn_Reiniciar`;
   - `Btn_Sair`.
6. Use textos curtos nos botoes:
   - `Continuar`;
   - `Reiniciar`;
   - `Sair`.

Nao e necessario criar arte final, animacao, configuracoes ou tela inicial para este teste.

## Conectar os botoes
No `Button > On Click ()`, arraste o GameObject `PauseMenu` que possui `PauseMenuController`.

Configure:
- `Btn_Continuar` -> `PauseMenuController.ContinueGame()`.
- `Btn_Reiniciar` -> `PauseMenuController.RestartScene()`.
- `Btn_Sair` -> `PauseMenuController.QuitGame()`.

## Comportamento do controlador
- `ESC` chama o toggle de pausa.
- Ao pausar, o painel aparece e `Time.timeScale` vai para `0`.
- Ao continuar, o painel some e `Time.timeScale` volta para `1`.
- Ao reiniciar a cena, o controlador restaura `Time.timeScale` antes de recarregar.
- Ao sair, o controlador restaura `Time.timeScale` antes de chamar a saida.
- No Editor, `Sair` mostra um `Debug.Log` em vez de fechar o Play Mode.

## Preparar reinicio de cena
1. Salve a cena antes do teste.
2. Inclua a cena no fluxo de build usado pelo prototipo para validar o mesmo comportamento em build.
3. Use `Reiniciar` depois de pausar para confirmar que a cena volta com o tempo normal.

## Testar no Editor
1. Entre em Play Mode.
2. Pressione `ESC`.
3. Confirme:
   - `PausePanel` aparece;
   - o movimento e a fisica param;
   - `Time.timeScale` fica em `0`.
4. Pressione `ESC` outra vez.
5. Confirme:
   - `PausePanel` some;
   - Rubens volta a responder;
   - `Time.timeScale` volta para `1`.
6. Pause novamente e clique `Continuar`.
7. Pause e clique `Reiniciar`.
8. Pause e clique `Sair`.
9. No Console do Editor, confira o log da solicitacao de saida.

## Problemas comuns
- `ESC` nao abre o painel: confira se `PauseMenu` esta ativo e se o componente esta habilitado.
- Os botoes nao recebem clique: confira o `EventSystem` e o `Graphic Raycaster` do Canvas.
- O painel inicia aberto: desative `PausePanel` antes do Play Mode ou confira a referencia usada pelo controlador.
- A cena nao reinicia em build: confira se ela esta incluida no fluxo de build do prototipo.

## Limites desta etapa
- Nao existe menu inicial nem retorno a menu principal.
- Nao existem configuracoes de audio, video ou controle.
- Nao ha save, transicao, confirmacao de saida ou visual final.
- O menu usa pausa por `Time.timeScale` para o teste atual.
