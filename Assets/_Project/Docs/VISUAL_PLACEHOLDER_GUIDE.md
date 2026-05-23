# VISUAL_PLACEHOLDER_GUIDE

## Objetivo
Definir a identidade visual placeholder da `Prototype_Bosque_Demo` sem usar assets externos, pacotes, URP ou arte final.

Este guia descreve a paleta, os sprites gerados por codigo, a leitura de colisao e como substituir esses placeholders por arte final no futuro.

## Paleta base do Bosque da Nevoa Perdida
| Uso | Cor base | Leitura desejada |
| --- | --- | --- |
| Chao principal | Verde musgo escuro | Solido, seguro e jogavel |
| Plataformas | Verde um pouco mais claro | Pisavel, mas secundario ao chao principal |
| Paredes solidas | Verde quase preto | Barreira fisica, sem promessa de caminho |
| Fundo distante | Verde/azul dessaturado com alpha baixo | Profundidade, sem colisao |
| Nevoa | Ciano claro translucidissimo | Atmosfera, sem esconder gameplay |
| Raizes inferiores | Azul profundo e roxo escuro | Area baixa, perigosa e fechada |
| DashGate | Ciano + roxo | Bloqueio especial ligado a habilidade |
| Checkpoint | Dourado/amarelo | Seguranca e retorno |
| Inimigo basico | Roxo vivo com contorno escuro | Hostil comum, diferente do chefe |
| Lucarelli | Vermelho/laranja com aura | Chefe e pico de perigo |
| Fim da demo | Dourado claro | Encerramento e recompensa |

## Sprites gerados
O builder cria automaticamente a pasta:

```text
Assets/_Project/Sprites/Generated
```

Sprites criados:
- `SP_Placeholder_Block.png`
- `SP_Placeholder_Ground.png`
- `SP_Placeholder_Platform.png`
- `SP_Placeholder_Wall.png`
- `SP_Placeholder_Trunk.png`
- `SP_Placeholder_TreeSilhouette.png`
- `SP_Placeholder_Fog.png`
- `SP_Placeholder_Checkpoint.png`
- `SP_Placeholder_DashGate.png`
- `SP_Placeholder_BasicEnemy.png`
- `SP_Placeholder_Lucarelli.png`
- `SP_Placeholder_Rubens.png`
- `SP_Placeholder_DemoEnd.png`
- `SP_Placeholder_Light.png`
- `SP_Placeholder_PitShadow.png`
- `M_PlaceholderSprites.mat`

Os arquivos sao PNGs simples gerados por `PrototypeSceneBuilder`. Eles usam formas, bordas, mascaras, gradientes leves e alpha para reduzir a sensacao de retangulos puros.

O builder so gera o PNG se ele ainda nao existir. Isso evita sobrescrever arte final caso esses arquivos sejam substituidos manualmente depois.

## Prefabs visuais basicos
O builder tambem cria prefabs visuais sem colisao em:

```text
Assets/_Project/Prefabs/VisualPlaceholders
```

Prefabs criados:
- `PF_Visual_Ground.prefab`
- `PF_Visual_Platform.prefab`
- `PF_Visual_Wall.prefab`
- `PF_Visual_Trunk.prefab`
- `PF_Visual_TreeSilhouette.prefab`
- `PF_Visual_Fog.prefab`
- `PF_Visual_Checkpoint.prefab`
- `PF_Visual_DashGate.prefab`
- `PF_Visual_BasicEnemy.prefab`
- `PF_Visual_Lucarelli.prefab`
- `PF_Visual_Rubens.prefab`
- `PF_Visual_DemoEnd.prefab`

Esses prefabs existem para inspecao e reuso rapido no Editor. A cena automatica ainda e montada diretamente pelo builder para manter o layout como fonte de verdade.

## Objetos com colisao
Tem colisao de gameplay:
- chao principal;
- plataformas;
- paredes solidas;
- paredes invisiveis;
- DashGate enquanto bloqueado;
- checkpoints com trigger;
- DeathZones com trigger;
- Rubens;
- inimigos;
- Lucarelli;
- marcador fisico do fim da demo.

Esses objetos devem continuar claros e com sorting acima do fundo.

## Objetos apenas visuais
Nao devem ter `Collider2D`:
- fundos;
- troncos distantes;
- copas e silhuetas;
- nevoa;
- luzes;
- pit shadows;
- landmarks visuais;
- placas visuais sem o trigger pai.

Se um objeto sem colisao parecer plataforma, ele precisa ficar mais transparente, mais escuro ou atras do gameplay.

## Sorting e leitura
O projeto nao cria Sorting Layers por codigo nesta etapa para evitar mexer demais no `TagManager`. O builder usa `sortingOrder` consistente:
- region markers ficam mais ao fundo;
- background fica atras;
- sombras de buraco ficam atras do gameplay;
- luzes e landmarks ficam atras ou abaixo dos objetos jogaveis;
- nevoa fica atras de Rubens, inimigos e plataformas;
- chao e plataformas ficam no nivel jogavel;
- checkpoints, gate, inimigos, Rubens, Lucarelli e sinais ficam acima.

## Como trocar por arte final depois
Opcoes seguras:
1. Substituir os PNGs gerados mantendo os mesmos nomes e caminhos.
2. Ajustar `PrototypeSceneBuilder.Helpers.cs` para apontar cada `PlaceholderSpriteKind` para outro asset.
3. Criar prefabs finais e trocar o builder para instanciar esses prefabs em vez dos placeholders.

Ao trocar, preserve:
- tamanho aproximado dos sprites;
- origem/pivot central;
- colisores atuais ou equivalentes;
- contraste entre objeto jogavel e fundo;
- visual distinto para checkpoint, DashGate, inimigo basico e Lucarelli.

## Regras de legibilidade
- Todo piso pisavel deve ter cor de gameplay e borda superior clara.
- Toda parede solida deve ser mais escura e sem borda de piso.
- Nevoa nunca deve cobrir Rubens ou esconder buraco importante.
- DeathZone nao deve parecer objeto fisico; o perigo deve vir da sombra do buraco.
- Checkpoint deve ser dourado e reconhecivel mesmo no fundo escuro.
- DashGate deve parecer especial antes de o jogador ter Dash.
- Lucarelli deve parecer maior e mais perigoso que inimigos comuns.
- O fim da demo deve ser legivel sem parecer uma saida falsa para outro mapa.

## Limites
Este sistema ainda e placeholder. Ele nao substitui arte final, animacao, particulas, parallax real, iluminacao 2D, audio, telegraphs finais ou polimento de boss.
