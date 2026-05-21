# MOVEMENT_SETUP

## Objetivo
Configurar Rubens para validar a movimentacao basica do prototipo em PC com `PlayerController2D` e `CameraFollow2D`.

## Criar `Rubens_Player`
1. Na cena de teste, crie um GameObject chamado `Rubens_Player`.
2. Adicione um `SpriteRenderer` com um sprite placeholder ou use um objeto visual filho simples.
3. Posicione o objeto acima de um piso com colisao antes de entrar em Play Mode.

## Componentes do jogador
Adicione ao `Rubens_Player`:
- `Rigidbody2D`.
- Um `Collider2D`, como `CapsuleCollider2D` ou `BoxCollider2D`.
- `PlayerController2D`.

O `PlayerController2D` exige `Rigidbody2D` e `Collider2D`, entao a Unity mantem esses componentes junto do script.

## Configurar `Rigidbody2D`
Use uma configuracao inicial simples:
- `Body Type`: `Dynamic`.
- `Simulated`: ligado.
- `Gravity Scale`: um valor maior que zero que fique bom na cena de teste.
- `Constraints`: congele `Rotation Z` para Rubens nao tombar ao colidir.

Mantenha o movimento horizontal e o pulo sob controle do `PlayerController2D` durante esta validacao.

## Configurar `Collider2D`
1. Ajuste o formato do collider ao volume do sprite placeholder.
2. Deixe `Is Trigger` desligado para o corpo colidir com o chao.
3. Garanta que o piso tambem tenha um `Collider2D` que nao seja trigger.

## Criar `GroundCheck`
1. Crie um GameObject filho de `Rubens_Player` chamado `GroundCheck`.
2. Posicione esse filho no centro dos pes, levemente abaixo do collider do jogador.
3. No `PlayerController2D`, arraste o `Transform` de `GroundCheck` para o campo `Ground Check`.
4. Comece com o `Ground Check Radius` padrao e ajuste apenas se o gizmo selecionado nao cobrir a area de contato com o piso.

O pulo so e liberado quando o circulo de `GroundCheck` encontra um collider incluido em `Ground Layer`.

## Configurar `Ground Layer`
1. Crie uma Layer chamada `Ground` em `Tags and Layers`, se ela ainda nao existir.
2. Atribua essa Layer aos GameObjects de piso e plataformas que devem liberar o pulo.
3. No `PlayerController2D`, marque `Ground` no campo `Ground Layer`.

Se o piso estiver fora da LayerMask, Rubens pode andar sobre ele, mas `IsGrounded` continuara falso e o pulo nao sera liberado.

## Configurar a camera
1. Selecione a `Main Camera`.
2. Adicione `CameraFollow2D`.
3. Arraste o `Transform` de `Rubens_Player` para `Target`.
4. Ajuste `Offset` para enquadrar o personagem. O valor inicial mantem a camera 2D em `Z = -10`.
5. Ajuste `Smooth Time` apenas se o acompanhamento estiver rigido ou atrasado demais para a sala de teste.

## Testar no Play Mode
1. Entre em Play Mode com Rubens sobre um piso da Layer `Ground`.
2. Use `A`/`D` ou as setas para validar `Input.GetAxisRaw("Horizontal")`.
3. Confirme que Rubens anda para os dois lados e vira para a direcao do movimento.
4. Pressione `Space` no chao para pular.
5. Pressione `Space` no ar e confirme que nenhum segundo pulo e disparado.
6. Observe se a camera acompanha Rubens com suavizacao durante deslocamento e pulo.
7. Se o pulo nao sair, selecione `Rubens_Player` e confira `Ground Check`, raio do gizmo, colliders do piso e `Ground Layer`.

## Limites desta etapa
Esta configuracao cobre apenas andar, pular, virar o personagem e seguir a camera. Combate, inimigos, checkpoints e Dash ficam fora desta validacao.
