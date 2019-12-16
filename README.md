# Treinamento Radix C#
  
## Cenário
  
  
Uma empresa de transporte de carga e pessoal faz algumas rotas de entrega em um
grupo de cidades locais. Dada uma simplificação do modelo, todas as rotas entre essas cidades são unidirecionais.
Portanto, mesmo que de fato exista uma via bidirecional entre duas cidades,
as suas rotas não necessariamente terão a mesma distância de ida e de volta.


## Objetivo


Essa empresa deseja portanto conhecer as melhores rotas entre algumas cidades 
e suas devidas distâncias com o intuito de definir a melhor logística de trabalho.
Para isso a radix foi contratada com o intuito de prover Endpoints REST de algumas funcionalidades comuns. 


## Critério de aceitação

A entrada será dada como um grafo direcionado onde um nó representa uma cidade e uma aresta representa uma rota entre duas cidades. 
O peso da aresta representa então a distância dessa rota.
Uma dada rota jamais aparecerá mais de uma vez, e para uma dada rota, as cidades de origem e destino sempre serão diferentes.

Uma rota direcionada será dada como um objeto JSON, onde as cidades serão nomeadas usando letras do alfabeto [A-Z]. 
Exemplo: uma rota de A para B com distância 5 é representada como:

```javascript
{ 
  "source": "A",
  "target": "B",
  "distance":5
}
```

![Exemplo gráfico do Grafo](Graph.png)

## Funcionalidades Esperadas (Especificação Funcional)

### Salvar Grafo

Esse endpoint deverá receber as arestas de um grafo e salva-las em um banco de dados para consultas posteriores.

* Endpoint: `http://localhost:5000/graph`
* HTTP Method: POST
* HTTP Success Response Code: CREATED (201)
* Contract:
  * Request payload

```javascript
{
  "data":[
    { 
      "source": "A", "target": "B", "distance":6
    },
    { 
      "source": "A", "target": "E", "distance":4
    },
    { 
      "source": "B", "target": "A", "distance":6
    },
    { 
      "source": "B", "target": "C", "distance":2
    },
    { 
      "source": "B", "target": "D", "distance":4
    },
    { 
      "source": "C", "target": "B", "distance":3
    },
    { 
      "source": "C", "target": "D", "distance":1
    },
    { 
      "source": "C", "target": "E", "distance":7
    },
    { 
      "source": "D", "target": "B", "distance":8
    },
    { 
      "source": "E",  "target": "B", "distance":5
    },
    { 
      "source": "E", "target": "D", "distance":7
    }
  ]
}
```

  * Response payload

```javascript
{
  "id" : 1,
  "data":[
    { 
      "source": "A", "target": "B", "distance":6
    },
    { 
      "source": "A", "target": "E", "distance":4
    },
    { 
      "source": "B", "target": "A", "distance":6
    },
    { 
      "source": "B", "target": "C", "distance":2
    },
    { 
      "source": "B", "target": "D", "distance":4
    },
    { 
      "source": "C", "target": "B", "distance":3
    },
    { 
      "source": "C", "target": "D", "distance":1
    },
    { 
      "source": "C", "target": "E", "distance":7
    },
    { 
      "source": "D", "target": "B", "distance":8
    },
    { 
      "source": "E",  "target": "B", "distance":5
    },
    { 
      "source": "E", "target": "D", "distance":7
    }
  ]
}
```

### Recuperar Grafo

Esse endpoint deverá retornar um grafo previamente salvo no banco de dados. Se o grafo não existe, deverá retornar HTTP NOT FOUND.

* Endpoint: `http://localhost:5000/graph/<graphId>`
* HTTP Method: GET
* HTTP Success Response Code: OK (200)
* HTTP Error Response Code: NOT FOUND (404)
* Contract:
  * Request payload: none

  * Response payload

```javascript
{
  "id" : 1,
  "data":[
    { 
      "source": "A", "target": "B", "distance":6
    },
    { 
      "source": "A", "target": "E", "distance":4
    },
    { 
      "source": "B", "target": "A", "distance":6
    },
    { 
      "source": "B", "target": "C", "distance":2
    },
    { 
      "source": "B", "target": "D", "distance":4
    },
    { 
      "source": "C", "target": "B", "distance":3
    },
    { 
      "source": "C", "target": "D", "distance":1
    },
    { 
      "source": "C", "target": "E", "distance":7
    },
    { 
      "source": "D", "target": "B", "distance":8
    },
    { 
      "source": "E",  "target": "B", "distance":5
    },
    { 
      "source": "E", "target": "D", "distance":7
    }
  ]
}
```

### Encontrar todas rotas disponíveis dada uma cidade de origem e outra de destino em um grafo salvo anteriormente

Utilizando um grafo salvo anteriormente, esse endpoint deverá calcular todas as rotas disponíveis de uma cidade origem para outra de destino, dado um número máximo de paradas. 
Se não existirem rotas possíveis, o resultado deverá ser uma lista vazia. Se o parâmetro "maxStops" não for definido, você deverá listar todas as rotas possíveis.
Se o grafo não existir, deverá retornar HTTP NOT FOUND.

Exemplo: No grafo (AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7), as possíveis rotas de A para C com máximo de 3 paradas seriam: ["ABC", "ADC", "AEBC"]

* Endpoint: `http://localhost:5000/routes/<graphId>/from/<town1>/to/<town2>/maxStops/<maxStops>`
* HTTP Method: POST
* HTTP Success Response Code: OK (200)
* HTTP Error Response Code: NOT FOUND (404)
* Contract:
  * Grafo salvo anteriormente

```javascript
{
  "data":[
    { 
      "source": "A", "target": "B", "distance":5
    },
    { 
      "source": "B", "target": "C", "distance":4
    },
    { 
      "source": "C", "target": "D", "distance":8
    },
    { 
      "source": "D", "target": "C", "distance":8
    },
    { 
      "source": "D", "target": "E", "distance":6
    },
    { 
      "source": "A", "target": "D", "distance":5
    },
    { 
      "source": "C", "target": "E", "distance":2
    },
    { 
      "source": "E", "target": "B", "distance":3
    },
    { 
      "source": "A", "target": "E", "distance":7
    }
  ]
}
```

  * Request payload: none
  * Response payload

```javascript
{
  "routes": [
    {
      "route": "ABC",
      "stops": 2
    },
    {
      "route": "ADC",
      "stops": 2
    },
    {
      "route": "AEBC",
      "stops": 3
    }
  ]
}
```


### Determinar a distância mínima entre duas cidades em um grafo salvo

Utilizando um grafo salvo anteriormente, esse endpoint deverá determinar a rota cuja distância seja a mínima possível entre duas cidades. 
Se as cidades de origem e destino forem iguais, o resultado deverá ser zero. 
Se não exitir rota possível entre as duas cidades, então o resultado deverá ser -1.
Se o grafo não existir, deverá retornar HTTP NOT FOUND.

* Endpoint: `http://localhost:5000/distance/<graphId>/from/<town1>/to/<town2>`
* HTTP Method: POST
* HTTP Success Response Code: OK (200)
* HTTP Error Response Code: NOT FOUND (404)
* Contract:
  * Grafo salvo anteriormente

```javascript
{
  "data":[
    { 
      "source": "A", "target": "B", "distance":6
    },
    { 
      "source": "A", "target": "E", "distance":4
    },
    { 
      "source": "B", "target": "A", "distance":6
    },
    { 
      "source": "B", "target": "C", "distance":2
    },
    { 
      "source": "B", "target": "D", "distance":4
    },
    { 
      "source": "C", "target": "B", "distance":3
    },
    { 
      "source": "C", "target": "D", "distance":1
    },
    { 
      "source": "C", "target": "E", "distance":7
    },
    { 
      "source": "D", "target": "B", "distance":8
    },
    { 
      "source": "E",  "target": "B", "distance":5
    },
    { 
      "source": "E", "target": "D", "distance":7
    }
  ]
}
```

  * Request payload: none

  * Response payload

```javascript
{
  "distance" : 8,
  "path" : ["A", "B", "C"]
}
```

## Dados para Teste

Seus testes automatizados devem contemplar no mínimo os seguintes casos:

Grafo Entrada:
AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7

Casos de Teste:
1. Rotas de origem C e destino C com um maximo de 3 paradas: 
	- C  (0 paradas)
2. Rotas de origem A e destino C com um maximo de 4 paradas:
	- ABC   (2 paradas)
	- ADC   (2 paradas)
	- AEBC  (3 paradas)
	- ADEBC (4 paradas)
3. Distância mínima de A para C: ABC  (distância = 9)
4. Distância mínima de B para B: B (distância = 0)


## Detalhes Técnicos

Para iniciar o desenvolvimento, favor criar um branch a partir da master.

O objetivo deste treinamento é mais do que implementar um simples algoritmo de solução. Iremos construir uma aplicação executável com uma estrutura mínima. Iremos modelar uma estrutura de objetos e utilizar Design Patterns sempre que apropriado e necessário, mas não se esqueça de manter tudo o mais simples possível. Alguns pacotes, classes e interfaces já estão no projeto como sugestão, mas você tem liberdade para alterar, renomear ou remover, caso se sinta mais confortável, mas ainda assim atenda padrões de uma arquitetura em camadas. 

1. A aplicação já contém a estrutura necessária para sua construção.
2. Garanta que sua suíte de testes faça parte da etapa de construção e esteja executando com sucesso. (Isso também já está configurado)
3. A aplicação pode ser inicializada pelo simples comando dotnet run ou pelo debug padrão (F5)
5. A aplicação deve conter a API stateless conforme especificação e usar um banco de dados local para salvar os dados quando necessário. 
6. Um banco de dados em memória já está disponível e configurado para utilização na base do projeto disponibilizado, através do EntityFramework.
7. A criação do banco de dados assim como sua estrutura de tabelas deverá acontecer de forma automática em tempo de execução (Isso também já está configurado, apenas garanta que sua modelagem seja feita de forma coerente). 

## Finalização do Desenvolvimento

Conforme forem concluidas as etapas do treinamento, crie um merge request em draft para o branch master. 
Você deve descrever minimamente sua implementação na descrição no merge request.

Sugerimos que trabalhe com commits organizados e envie pequenas alterações para sua branch sempre que tiver o código e aplicação estáveis para execução.

## Pontos importantes ao escrever o código

1. Performance e correta execução da especificação funcional
2. Legibilidade de código e consistência de nomenclaturas
3. Modelagem e OO (Orientação à Objetos)
4. Testes Unitários
5. Uso apropriado do C#, assim como as frameworks e suas melhores práticas
