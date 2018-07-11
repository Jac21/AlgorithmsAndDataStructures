/* doubly-linked list implementation */
#include <stdio.h>
#include <stdlib.h>

struct List
{
	struct MNode *head;
	struct MNode *tail;
	struct MNode *tail_predecessor;
};

struct MNode
{
	struct MNode *successor;
	struct MNode *predecessor;
};

typedef struct MNode *NODE;
typedef struct List *LIST;

/*
** LIST l = newList()
** create (alloc space for) and initialize a list
*/
LIST newList(void);

/*
** int isEmpty(LIST l)
** test if a list is empty
*/
int isEmpty(LIST);

/*
** NODE n = getTail(LIST l)
** get the tail node of the list, without removing it
*/
NODE getTail(LIST);

/*
** NODE n = getHead(LIST l)
** get the head node of the list, without removing it
*/
NODE getHead(LIST);

/*
** NODE rn = addTail(LIST l, NODE n)
** add the node n to the tail of the list l, and return it (rn==n)
*/
NODE addTail(LIST, NODE);

/*
** NODE n = remHead(LIST l)
** remove the head node of the list and return it
*/
NODE remHead(LIST);

/*
** NODE n = remTail(LIST l)
** remove the tail node of the list and return it
*/
NODE remTail(LIST);

/*
** NODE rn = insertAfter(LIST l, NODE r, NODE n)
** insert the node n after the node r, in the list l; return n (rn==n)
*/
NODE insertAfter(LIST, NODE, NODE);

/*
** NODE rn = removeNode(LIST l, NODE n)
** remove the node n (that must be in the list l) from the list and return it (rn==n)
*/
NODE removeNode(LIST, NODE);

LIST newList(void)
{
	// allocate space for list struct
	LIST tl = malloc(sizeof(struct List));
	if (tl != NULL)
	{
		tl->tail_predecessor = (NODE)&tl->head;
		tl->tail = NULL;
		tl->head = (NODE)&tl->tail;
		return tl;
	}
	return NULL;
}

int isEmpty(LIST l)
{
	return (l->head->successor == 0);
}

NODE getHead(LIST l)
{
	return l->head;
}

NODE getTail(LIST l)
{
	return l->tail_predecessor;
}

NODE addTail(LIST l, NODE n)
{
	n->successor = (NODE)&l->tail;
	n->predecessor = l->tail_predecessor;
	l->tail_predecessor->successor = n;
	l->tail_predecessor = n;
	return n;
}

NODE addHead(LIST l, NODE n)
{
	n->successor = l->head;
	n->predecessor = (NODE)&l->head;
	l->head->predecessor = n;
	l->head = n;
	return n;
}

NODE remHead(LIST l)
{
	NODE h;
	h = l->head;
	l->head = l->head->successor;
	l->head->predecessor = (NODE)&l->head;
	return h;
}

NODE remTail(LIST l)
{
	NODE t;
	t = l->tail_predecessor;
	l->tail_predecessor = l->tail_predecessor->predecessor;
	l->tail_predecessor->successor = (NODE)&l->tail;
	return t;
}

NODE insertAfter(LIST l, NODE r, NODE n)
{
	n->predecessor = r;
	n->successor = r->successor;
	n->successor->predecessor = n;
	r->successor = n;
	return n;
}

NODE removeNode(LIST l, NODE n)
{
	n->predecessor->successor = n->successor;
	n->successor->predecessor = n->predecessor;
	return n;
}

// basic test
struct IntNode
{
	struct MNode node;
	int data;
};

int main()
{
	int i;
	LIST list_a;
	struct IntNode *m;
	struct IntNode *o;

	list_a = newList();

	printf("Created new list structure, size: %d\n", sizeof(list_a));

	if (list_a != NULL)
	{
		for (i = 0; i < 5; i++)
		{
			m = malloc(sizeof(struct IntNode));
			o = malloc(sizeof(struct IntNode));

			printf("Created new IntNodes m, size: %d\n", sizeof(m));
			printf("Created new IntNode o, size: %d\n", sizeof(o));

			if (m != NULL && o != NULL)
			{
				m->data = rand() % 64;
				o->data = rand() % 64;
				addTail(list_a, (NODE)m);
				insertAfter(list_a, (NODE)m, (NODE)o);
			}
		}

		while (!isEmpty(list_a))
		{
			m = (struct IntNode *)remTail(list_a);

			printf("Node data: %d\n", m->data);

			free(m);
		}

		free(list_a);

		printf("De-allocated list structure, size: %d\n", sizeof(list_a));
	}
}