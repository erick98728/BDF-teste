# SCENE_SETUP (Teste de Movimentação do Rubens)

## 1) Criar cena de teste
1. Em `Assets/_Project/Scenes`, crie uma cena chamada `Prototype_MovementTest`.
2. Salve a cena.

## 2) Criar o chão
1. Crie um `GameObject > 2D Object > Sprite > Square` e renomeie para `Ground`.
2. Ajuste `Scale` para algo como `X = 20`, `Y = 1`, `Z = 1`.
3. Posicione em `Y = -2` (ou equivalente).
4. Adicione/garanta `BoxCollider2D`.
5. Crie a Layer `Ground` e atribua ao objeto `Ground`.

## 3) Criar Rubens (placeholder)
1. Crie um `GameObject > 2D Object > Sprite > Square` e renomeie para `Rubens`.
2. Adicione `Rigidbody2D` com:
   - `Gravity Scale` entre `3` e `5`.
   - `Freeze Rotation Z` habilitado.
3. Garanta um `Collider2D` (ex.: `BoxCollider2D`).
4. Adicione script `PlayerController2D`.

## 4) Configurar Ground Check
1. Crie um filho em `Rubens` chamado `GroundCheck`.
2. Posicione o `GroundCheck` levemente abaixo dos pés (ex.: `Y = -0.55`).
3. No `PlayerController2D`, arraste esse transform para o campo `Ground Check`.
4. Defina `Ground Layer` para a layer `Ground`.
5. Ajuste `Ground Check Radius` (ex.: `0.15` a `0.25`).

## 5) Configurar Câmera
1. Na `Main Camera`, adicione script `CameraFollow2D`.
2. Arraste `Rubens` para o campo `Target`.
3. Ajuste `Offset` se necessário (sugestão: `0, 1, -10`).
4. Ajuste `Smooth Time` (sugestão: `0.1` a `0.2`).

## 6) Teste rápido
- Rode a cena (`Play`).
- `A/D` ou setas esquerda/direita: movimentar Rubens.
- `Space`: pular (somente no chão).
- Observe se a câmera segue suavemente.

## 7) Ajustes finos no Inspector
- `Move Speed`: velocidade lateral.
- `Jump Force`: altura/força do pulo.
- `Ground Check Radius`: sensibilidade de detecção de chão.
- `Smooth Time`: suavidade da câmera.
